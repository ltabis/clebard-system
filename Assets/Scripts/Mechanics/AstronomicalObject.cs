using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronomicalObject : MonoBehaviour
{
    private bool isInitialized = false;

    public string objectName = "unknown";
    public float mass;
    public Vector3 initialVelocity = new Vector3();
    private Vector3 velocity;
    private Rigidbody rb;

    private void Awake()
    {
        velocity = initialVelocity;
        rb = GetComponent<Rigidbody>();

        isInitialized = true;
    }

    // update the velocity of the body using
    // all of the solar system's content.
    public void UpdateVelocity(AstronomicalObject[] objects, float timeSpeed)
    {
        // no need to redeclare all variables for each loop.
        Vector3 distance, force, acceleration;

        foreach (var other in objects) {

            if (other != this) {
                // calculating the force to get the current velocity of the body.
                // G(m1m2/r2)
                distance = other.Position - rb.position;
                force = distance.normalized * mass * other.mass / distance.sqrMagnitude;
                acceleration = force / mass;

                velocity += acceleration * timeSpeed;
            }
        }
    }

    // Update the current position of the body
    // using the current velocity.
    public void UpdatePosition(float timeSpeed)
    {
        rb.position += velocity * timeSpeed;
    }

    // Getters.

    public Vector3 Velocity
    {
        get {
            return velocity;
        }
    }

    public Vector3 Position
    {
        get {
            return rb.position;
        }
    }

    public bool IsInitialized
    {
        get {
            return isInitialized;
        }
    }

    public void SetPosition(Vector3 pos)
    {
        rb.position = pos;
    }
}
