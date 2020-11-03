using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour {

    public float distanceThreshold = 1100;
    List<Transform> physicsObjects;
    public GameObject player;
    Camera playerCamera;

    public event System.Action PostFloatingOriginUpdate;

    void Awake() {
        var bodies = FindObjectsOfType<AstronomicalObject>();

        physicsObjects = new List<Transform>();
        physicsObjects.Add(player.transform);
        foreach (var c in bodies) {
            physicsObjects.Add(c.transform);
        }
    }

    void LateUpdate() {
        if (player) {
            UpdateFloatingOrigin();
            if (PostFloatingOriginUpdate != null) {
                PostFloatingOriginUpdate();
            }
        }
    }

    void UpdateFloatingOrigin() {
        Vector3 originOffset = player.gameObject.transform.position;
        float dstFromOrigin = originOffset.magnitude;
        if (dstFromOrigin > distanceThreshold) {
            foreach (Transform t in physicsObjects) {
                t.position -= originOffset;
            }
        }
    }

}