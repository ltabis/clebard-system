using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoClipController : MonoBehaviour
{
    public Camera cam;
    public float camSpeed = 1.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        ComputeInput();
        FollowCursor();
    }

    private void ComputeInput()
    {
        if (Input.GetButton("up"))
            transform.position -= transform.forward * camSpeed * Time.deltaTime;
        if (Input.GetButton("down"))
            transform.position += transform.forward * camSpeed * Time.deltaTime;
        if (Input.GetButton("right"))
            transform.position -= transform.right * camSpeed * Time.deltaTime;
        if (Input.GetButton("left"))
            transform.position += transform.right * camSpeed * Time.deltaTime;
        if (Input.GetButton("crouch"))
            transform.position -= transform.up * camSpeed * Time.deltaTime;
        if (Input.GetButton("jump"))
            transform.position += transform.up * camSpeed * Time.deltaTime;
    }

    private void FollowCursor()
    {
        yaw += Input.GetAxis("Mouse X");
        pitch += Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }
}
