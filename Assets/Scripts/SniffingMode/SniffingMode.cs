using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SniffingMode : MonoBehaviour {
    public Camera mainCamera;
    public Camera ppCamera;
    private UniversalAdditionalCameraData mainCameraURP;
    private UniversalAdditionalCameraData ppCameraURP;
    public bool sniffingMode;

    void Start() {
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
            }
            else {
                mainCameraURP.renderPostProcessing = true;
                ppCameraURP.renderPostProcessing = true;
            }
            sniffingMode = !sniffingMode;
        }
    }
}