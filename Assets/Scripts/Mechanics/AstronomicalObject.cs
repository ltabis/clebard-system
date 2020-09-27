using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronomicalObject : MonoBehaviour
{
    public string objectName = "unknown";
    public float mass;
    public float radius;
    public Vector3 initialVelocity = new Vector3();
    private Vector3 velocity;
    private Rigidbody rb;

    private void Awake()
    {
        velocity = initialVelocity;
        rb = GetComponent<Rigidbody>();
    }

    public void UpdateVelocity(AstronomicalObject[] objects, float timeSpeed)
    {
        // no need to redeclare all variables for each loop.
        Vector3 distance, force, acceleration;

        foreach (var other in objects) {

            if (other != this) {

                // calculating the force to get the current velocity of the body.
                // G(m1m2/r2)
                distance = other.GetComponent<Rigidbody>().position - rb.position;
                force = distance.normalized * mass * other.mass / distance.sqrMagnitude;
                acceleration = force / mass;

                velocity += acceleration * timeSpeed;
            }
        }
    }

    public void UpdatePosition(float timeSpeed)
    {
        rb.MovePosition(rb.position + velocity * timeSpeed);
    }

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
}
