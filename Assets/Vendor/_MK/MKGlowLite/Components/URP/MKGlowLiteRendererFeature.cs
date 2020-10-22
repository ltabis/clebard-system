//////////////////////////////////////////////////////
// MK Glow URP Renderer Feature	    		        //
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2020 All rights reserved.            //
//////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MK.Glow.URP
{
    public class MKGlowLiteRendererFeature : ScriptableRendererFeature
    {
        class MKGlowLiteRenderPass : ScriptableRenderPass
        {
            private MK.Glow.URP.MKGlowLite _mKGlowVolumeComponent;
            private MK.Glow.URP.MKGlowLite mKGlowVolumeComponent
            {
                get
                {
                    _mKGlowVolumeComponent = _mKGlowVolumeComponent ?? VolumeManager.instance.stack.GetComponent<MK.Glow.URP.MKGlowLite>();
                    return _mKGlowVolumeComponent;
                }
            }

            internal Effect effect = new Effect();
            internal RenderTarget sourceRenderTarget, destinationRenderTarget;
            private SettingsURP _settingsURP;
            private RenderTextureDescriptor _sourceDescriptor;
            private readonly int _rendererBufferID = Shader.PropertyToID("_MKGlowLiteScriptableRendererOutput");
            private readonly string _profilerName = "MKGlowLite";

            public MKGlowLiteRenderPass()
            {
                this.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            }

            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
                _sourceDescriptor = cameraTextureDescriptor;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                _settingsURP = mKGlowVolumeComponent;
                float renderScale = 1;
                if(renderingData.cameraData.camera.cameraType != CameraType.SceneView)
                {
                    renderScale = renderingData.cameraData.renderScale;
                }

                CommandBuffer cmd = CommandBufferPool.Get(_profilerName);

                destinationRenderTarget.identifier = _rendererBufferID;
                #if UNITY_2018_2_OR_NEWER
                destinationRenderTarget.renderTargetIdentifier = new RenderTargetIdentifier(destinationRenderTarget.identifier, 0, CubemapFace.Unknown, -1);
                #else
                destinationRenderTarget.renderTargetIdentifier = new RenderTargetIdentifier(destinationRenderTarget.identifier);
                #endif
                cmd.GetTemporaryRT(destinationRenderTarget.identifier, _sourceDescriptor, FilterMode.Bilinear);
                Blit(cmd, sourceRenderTarget.renderTargetIdentifier, destinationRenderTarget.renderTargetIdentifier);
                effect.Build(destinationRenderTarget, sourceRenderTarget, _settingsURP, cmd, renderingData.cameraData.camera, renderScale);
                cmd.ReleaseTemporaryRT(destinationRenderTarget.identifier);

                context.ExecuteCommandBuffer(cmd);

                cmd.Clear();
                CommandBufferPool.Release(cmd);
            }

            /*
            public override void FrameCleanup(CommandBuffer cmd)
            {
            }
            */
        }

        private MKGlowLiteRenderPass _mkGlowRenderPass;
        private readonly string _componentName = "MKGlowLite";

        public override void Create()
        {
            _mkGlowRenderPass = new MKGlowLiteRenderPass();
            _mkGlowRenderPass.effect.Enable(RenderPipeline.SRP);
        }

        private void OnDisable()
        {
            _mkGlowRenderPass.effect.Disable();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            name = _componentName;

            if(renderingData.cameraData.postProcessEnabled)
            {
                _mkGlowRenderPass.sourceRenderTarget.renderTargetIdentifier = renderer.cameraColorTarget;
                renderer.EnqueuePass(_mkGlowRenderPass);
            }
        }
    }
}


