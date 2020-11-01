using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float mouseXSensitivity = 1;
    public float mouseYSensitivity = 1;
    public float moveSpeed = 6;
    private Vector3 moveDirection;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private Rigidbody rb;
    Transform cameraTransform;
    float verticalLookRotation;
    public LayerMask groundedMask;
    private bool grounded;

    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        /*transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseXSensitivity);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseYSensitivity;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;*/
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)) {
            grounded = true;
        } else {
            grounded = false;
        }
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(inputX, 0, inputY).normalized;
        Vector3 targetMoveAmount = moveDirection * moveSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.15f);

        if (Input.GetButtonDown("Jump")) {
            if (grounded)
                rb.AddForce(transform.up * 100);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (grounded) {
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + localMove);
        }
    }
}