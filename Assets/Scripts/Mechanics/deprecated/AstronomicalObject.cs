using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronomicalObject : MonoBehaviour
{
    public float mass;
    public Vector3 initialVelocity;
    private Vector3 velocity;
    private Rigidbody rb;

    private void Awake()
    {
        velocity = initialVelocity;
        rb = GetComponent<Rigidbody>();
    }

    // update the velocity of the body using
    // all of the solar system's content.
    public void UpdateVelocity(AstronomicalObject[] bodies, float timeSpeed)
    {
        // no need to redeclare all variables for each loop.
        Vector3 distance, acceleration;

        foreach (var other in bodies) {

            if (other != this) {
                // calculating the force to get the current velocity of the body.
                // G(m1m2/r2)
                distance = other.Position - Position;
                acceleration = Universe.GravitationalConstant * distance.normalized * other.mass / distance.sqrMagnitude;

                velocity += acceleration * timeSpeed;
            }
        }
    }

    // Update the current position of the body
    // using the current velocity.
    public void UpdatePosition(float timeSpeed)
    {
        rb.MovePosition(rb.position + velocity * timeSpeed);
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
            if (!rb)
                rb = GetComponent<Rigidbody>();
            return rb.position;
        }
    }
}
