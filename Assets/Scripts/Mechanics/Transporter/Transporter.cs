using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    [SerializeField]
    private bool isActive = true;

    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;
    [SerializeField]
    private ParticleSystem PSA;
    [SerializeField]
    private ParticleSystem PSB;
    [SerializeField]
    private float transitionTime = 10f;

    private ParticleSystem particleTunnel;
    private CapsuleCollider colliderTunnel;

    private GameObject AttractedBody;
    private NPlayerController AttractedBodyGravityScript;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Transform end;

    private bool bodyIsBeingAttracted = false;
    private float lerpPct = 0f;

    void Start()
    {
        particleTunnel = gameObject.GetComponent<ParticleSystem>();
        colliderTunnel = gameObject.GetComponent<CapsuleCollider>();

        // direction of the tunnel.
        Vector3 direction = pointA.position - pointB.position;
        float distance = direction.magnitude;

        // create the particle system for the tunnel effect.
        ParticleSystem.ShapeModule shape = particleTunnel.shape;

        // the length of the tunnel is equal to the distance between plateforms.
        shape.length = distance;

        // aliging the particle system in the direction of pointB starting from pointA.
        transform.position = pointA.position;
        transform.forward = -direction;

        // adjusting the collider to fit the transporter.
        colliderTunnel.direction = 2;
        colliderTunnel.isTrigger = true;
        colliderTunnel.center = Vector3.forward * (distance / 2);
        colliderTunnel.height = distance;
        colliderTunnel.radius = 1.5f;

        if (!isActive)
            EnableParticles(false);
    }

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
            if (lerpPct >= 1 && AttractedBodyGravityScript) {
                AttractedBodyGravityScript.EnableVelocity(true);
                AttractedBodyGravityScript = null;
            }

            // reseting default values.
            lerpPct = 0;
            bodyIsBeingAttracted = false;            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive)
            return;

        AttractedBody = other.gameObject;

        // computing the point nearest to the player.
        float nearA = (AttractedBody.transform.position - pointA.position).sqrMagnitude;
        float nearB = (AttractedBody.transform.position - pointB.position).sqrMagnitude;

        // the player will be teleported to the nearest point.
        // for the start pos we do not use a transform because if the player
        // transform is copied then the start point updates with the player position.
        startPosition = AttractedBody.transform.position;
        startRotation = AttractedBody.transform.rotation;
        end = nearA > nearB ? pointA : pointB;

        // start lerping the player toward the other point.
        bodyIsBeingAttracted = true;

        // deactivate gravity script for the player.
        NPlayerController PlayerGravityScript = AttractedBody.GetComponent<NPlayerController>();

        if (PlayerGravityScript) {
            AttractedBodyGravityScript = PlayerGravityScript;
            AttractedBodyGravityScript.EnableVelocity(false);
        }
    }

    public bool Active
    {
        get { return isActive; }
        set {
            isActive = value;
            if (isActive) {
                EnableParticles(true);
            } else {
                EnableParticles(false);
                if (AttractedBodyGravityScript)
                    AttractedBodyGravityScript.EnableVelocity(true);
            }
        }
    }

    void EnableParticles(bool state)
    {
        if (state) {
            PSA.Play();
            PSB.Play();
            particleTunnel.Play();
        } else {
            PSA.Stop();
            PSB.Stop();
            particleTunnel.Stop();
        }
    }
}
