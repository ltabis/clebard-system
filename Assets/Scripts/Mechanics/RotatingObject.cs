using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
    public Vector3 rotationAxis = new Vector3(1.0f, 0.0f, 0.0f);

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = rotationAxis * rotateSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis * rotateSpeed * Time.deltaTime);

        // if (!Application.isPlaying)
        //     Debug.DrawLine(rotation);
    }
}
