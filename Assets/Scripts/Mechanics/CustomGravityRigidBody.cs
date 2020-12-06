using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravityRigidBody : MonoBehaviour
{
    [SerializeField]
    bool isAbleToSleep = false;
    private Rigidbody rb;
    // delay from wich the object isn't moving but is maybe floating.
    private float delay = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (isAbleToSleep) {
            // prevent PhysicX to overwork.
            if (rb.IsSleeping()) {
                delay = 0f;
                return;
            }

            // check if the object is moving.
            if (rb.velocity.magnitude < 0.001f) {
                delay += Time.deltaTime;
                if (delay >= 1f)
                    return;
            } else
                delay = 0f;
        }

        // add force following the current custom gravity direction.
        rb.AddForce(CustomGravity.GetGravity(rb.position), ForceMode.Acceleration);
    }
}