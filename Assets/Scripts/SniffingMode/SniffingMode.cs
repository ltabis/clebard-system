using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SniffingMode : MonoBehaviour {
    private Camera mainCamera;
    private UniversalAdditionalCameraData postProcessCamera;
    public bool sniffingMode;

    void Start() {
        mainCamera = Camera.main;
        postProcessCamera = mainCamera.GetComponent<UniversalAdditionalCameraData>();
        sniffingMode = false;
    }

    void Update() {
        SniffingModeCheck();
    }

    void SniffingModeCheck() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if (sniffingMode)
                postProcessCamera.renderPostProcessing = false;
            else
                postProcessCamera.renderPostProcessing = true;
            sniffingMode = !sniffingMode;
        }
    }
}