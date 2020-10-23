using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] public class RotatingObject : MonoBehaviour
{
    public float raySize = 5.0f;
    public float rotateSpeed = 1.0f;
    public Vector3 rotationAxis = new Vector3(1.0f, 0.0f, 0.0f);

    void Update()
    {
        Vector3 rotation = rotationAxis * rotateSpeed * Time.deltaTime;

        if (Application.isPlaying)
            transform.Rotate(rotation);
        else
            Debug.DrawLine(transform.position,  (rotationAxis + rotation) * raySize, Color.red, 0.1f);
    }
}
