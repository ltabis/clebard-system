using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravityRigidBody : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        rb.AddForce(CustomGravity.GetGravity(rb.position), ForceMode.Acceleration);
    }
}