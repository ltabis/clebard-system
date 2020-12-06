using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float vSmoothTime = 0.1f;
    public float airSmoothTime = 0.5f;
    public Transform feet;
    public Transform body;
    public Animator anim;
    public OrbitCamera cam;
    private Rigidbody rb;
    private AstronomicalObject[] objects;
    private AstronomicalObject referenceBody;
    Vector3 smoothVelocity;
    Vector3 smoothVRef;
    Vector3 targetVelocity;
    public LayerMask groundedMask;
    public float moveSpeed = 6f;

    public float mouseSensitivity = 10f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSmoothTime = 0.1f;

    public float yaw;
    public float pitch;
    float smoothYaw;
    float smoothPitch;

    float yawSmoothV;
    float pitchSmoothV;

    void Start() {
        objects = FindObjectsOfType<AstronomicalObject>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Movement() {
        // Movement.
        bool isGrounded = IsGrounded();

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        targetVelocity = transform.TransformDirection(input.normalized) * moveSpeed;
        smoothVelocity = Vector3.SmoothDamp(smoothVelocity, targetVelocity, ref smoothVRef, (isGrounded) ? vSmoothTime : airSmoothTime);
        if (isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                rb.AddForce(transform.up * 10f, ForceMode.VelocityChange);
                isGrounded = false;
                anim.Play("Jump");
            } else {
                if (smoothVelocity != Vector3.zero && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    anim.Play("Walk");
                rb.AddForce(-transform.up * 1f, ForceMode.VelocityChange);
            }
        } else
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                anim.Play("Run");

        // Player body orientation.
        if (input.x != 0 || input.z != 0) {
            // Vector2 playerInput;
            // playerInput.x = Input.GetAxis("Horizontal");
            // playerInput.y = Input.GetAxis("Vertical");
            // playerInput = Vector2.ClampMagnitude(playerInput, 1f);

            // Vector3 forward = body.position - cam.transform.position;
            // forward.Normalize();
            // Vector3 right = cam.transform.right;
            // right.Normalize();

            // body.transform.forward = forward;
            // body.transform.right = right;
            // rotating the player body only if the user is moving.
            // yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            // float smoothYawOld = smoothYaw;
            // smoothYaw = Mathf.SmoothDampAngle(smoothYaw, yaw, ref yawSmoothV, rotationSmoothTime);
            // body.transform.Rotate(Vector3.up * Mathf.DeltaAngle(smoothYawOld, smoothYaw), Space.Self);

            // body.forward = new Vector3(cam.transform.forward.x, cam.transform.forward.y, cam.transform.forward.z);
        }
    }

    private void Update() {
        Movement();
    }

    private void FixedUpdate() {
        Vector3 strongestPull = Vector3.zero;

        foreach (var obj in objects) {
            float sqrDst = (obj.Position - rb.position).sqrMagnitude;
            Vector3 forceDir = (obj.Position - rb.position).normalized;
            Vector3 acceleration = forceDir * Universe.GravitationalConstant * obj.mass / sqrDst;
            rb.AddForce(acceleration, ForceMode.Acceleration);

            if (acceleration.magnitude > strongestPull.sqrMagnitude) {
                strongestPull = acceleration;
                referenceBody = obj;
            }
        }

        Vector3 gravityUp = -strongestPull.normalized;
        rb.rotation = Quaternion.FromToRotation(transform.up, gravityUp) * rb.rotation;
        rb.MovePosition(rb.position + smoothVelocity * Time.deltaTime);
    }

    private bool IsGrounded() {
        // Sphere must not overlay terrain at origin otherwise no collision will be detected
        // so rayRadius should not be larger than controller's capsule collider radius
        const float rayRadius = 5f;
        const float groundedRayDst = 2f;
        bool grounded = false;

        if (referenceBody) {
            var relativeVelocity = rb.velocity - referenceBody.Velocity;
            // Don't cast ray down if player is jumping up from surface
            if (relativeVelocity.y <= 12f * .5f) {
                RaycastHit hit;
                Vector3 offsetToFeet = (feet.position - transform.position);
                Vector3 rayOrigin = rb.position + offsetToFeet + transform.up * rayRadius;
                Vector3 rayDir = -transform.up;
                grounded = Physics.SphereCast(rayOrigin, rayRadius, rayDir, out hit, groundedRayDst, groundedMask);
            }
        }

        return grounded;
    }
}