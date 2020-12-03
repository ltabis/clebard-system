using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SniffingMode : MonoBehaviour {
    private Camera mainCamera;
    private Camera ppCamera;
    private UniversalAdditionalCameraData mainCameraURP;
    private UniversalAdditionalCameraData ppCameraURP;
    public GameObject palmTree;
    public bool sniffingMode;

    void Start() {
        mainCamera = Camera.main;
        ppCamera = GameObject.FindGameObjectWithTag("PPCamera").GetComponent<Camera>();
        mainCameraURP = mainCamera.GetComponent<UniversalAdditionalCameraData>();
        ppCameraURP = ppCamera.GetComponent<UniversalAdditionalCameraData>();
        sniffingMode = false;
    }

    void Update() {
        SniffingModeCheck();
    }

    void SniffingModeCheck() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if (sniffingMode) {
                mainCameraURP.renderPostProcessing = false;
                ppCameraURP.renderPostProcessing = false;
                palmTree.layer = LayerMask.NameToLayer("Default");
            }
            else {
                mainCameraURP.renderPostProcessing = true;
                ppCameraURP.renderPostProcessing = true;
                palmTree.layer = LayerMask.NameToLayer("PostProcessing");
            }
            sniffingMode = !sniffingMode;
        }
    }
}