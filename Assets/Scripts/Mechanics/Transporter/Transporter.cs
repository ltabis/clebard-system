using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;
    [SerializeField]
    private float transitionTime = 10f;

    private GameObject AttractedBody;
    private MonoBehaviour AttractedBodyGravityScript;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Transform end;

    private bool bodyIsBeingAttracted = false;
    private bool isActive = true;
    private float lerpPct = 0f;

    void Update()
    {
        if (bodyIsBeingAttracted && lerpPct < 1 && isActive) {

            // interpolate the body from start to end.
            AttractedBody.transform.position = Vector3.Lerp(startPosition, end.position, lerpPct);
            AttractedBody.transform.rotation = Quaternion.Lerp(startRotation, end.rotation, lerpPct);

            // updating the position using a time scale.
            lerpPct += Time.deltaTime / transitionTime;
        } else {

            // enabling custom gravity once again.
            if (lerpPct >= 1)
                AttractedBodyGravityScript.enabled = true;
            
            // reseting default values.
            lerpPct = 0;
            bodyIsBeingAttracted = false;            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        AttractedBody = other.gameObject;

        // computing the point nearest to the player.
        float nearA = (AttractedBody.transform.position - pointA.position).sqrMagnitude;
        float nearB = (AttractedBody.transform.position - pointB.position).sqrMagnitude;

        // the player will be teleported to the nearest point.
        startPosition = AttractedBody.transform.position;
        startRotation = AttractedBody.transform.rotation;
        end = nearA > nearB ? pointA : pointB;

        // start lerping the player toward the other point.
        bodyIsBeingAttracted = true;

        // deactivate gravity script.
        MonoBehaviour gameObjectGravityScript = AttractedBody.GetComponent<CustomGravityRigidBody>();
        MonoBehaviour PlayerGravityScript = AttractedBody.GetComponent<NPlayerController>();

        if (gameObjectGravityScript)
            AttractedBodyGravityScript = gameObjectGravityScript;
        else if (PlayerGravityScript)
            AttractedBodyGravityScript = PlayerGravityScript;

        AttractedBodyGravityScript.enabled = false;
    }

    public bool Active
    {
        get { return isActive; }
        set {
            isActive = value;
        }
    }
}
