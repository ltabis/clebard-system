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

    private List<GameObject> importantItems;

    void Start() {
        mainCameraURP = mainCamera.GetComponent<UniversalAdditionalCameraData>();
        ppCameraURP = ppCamera.GetComponent<UniversalAdditionalCameraData>();
        sniffingMode = false;
        GetImportantItems();
    }


    void GetImportantItems()
    {
        importantItems = new List<GameObject>();
        GameObject[] go = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in go)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Focused"))
                importantItems.Add(gameObject);
        }
    }

    void Update() {
        SniffingModeCheck();
    }

    void SniffingModeCheck() {
        if (Input.GetKeyDown(KeyCode.A)) {
            if (sniffingMode) {
                mainCameraURP.renderPostProcessing = false;
                ppCameraURP.renderPostProcessing = false;
                foreach (GameObject item in importantItems)
                    item.layer = LayerMask.NameToLayer("Focused");
            }
            else {
                mainCameraURP.renderPostProcessing = true;
                ppCameraURP.renderPostProcessing = true;
                foreach (GameObject item in importantItems)
                    item.layer = LayerMask.NameToLayer("PostProcessing");
            }
            sniffingMode = !sniffingMode;
        }
    }
}