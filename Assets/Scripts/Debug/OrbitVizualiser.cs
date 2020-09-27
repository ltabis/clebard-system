using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitVizualiser : MonoBehaviour
{
    public float mass;
    public float radius;
    public Vector3 velocity;
    public Vector3 position;

    public bool debug = true;
    public float timeSpeed = 1;
    public uint iterations = 1;
    private AstronomicalObject reference;
    private AstronomicalObject[] allBodies;

    private void Start()
    {
        Debug.DrawLine(Vector3.zero, new Vector3(5, 0, 0), Color.white, 2.5f);
        allBodies = FindObjectsOfType<AstronomicalObject>();
        reference = GetComponent<AstronomicalObject>();

        mass = reference.mass;
        radius = reference.radius;
        velocity = reference.Velocity;
        position = reference.Position;
    }

    private void Update()
    {
        if (debug)
            Simulate();
    }

    private void Simulate()
    {
        Vector3 currentPosition = position,
                previousPosition = position,
                initialPosition = position,
                initialVelocity = velocity;

        for (uint i = 0; i < iterations; ++i) {
            // Debug.Log("Simulating ... (" + i + "/" + iterations + ")");
            UpdateVelocity(allBodies, timeSpeed);
            UpdatePosition(timeSpeed);

            currentPosition = position;
            Debug.DrawLine(previousPosition, currentPosition, Color.red, 1);
            previousPosition = currentPosition;
        }

        position = initialPosition;
        velocity = initialVelocity;
    }

    public void UpdateVelocity(AstronomicalObject[] objects, float timeSpeed)
    {
        // no need to redeclare all variables for each loop.
        Vector3 distance, force, acceleration;

        foreach (var other in objects) {
            if (other != reference) {
                // calculating the force to get the current velocity of the body.
                // G(m1m2/r2)
                distance = other.GetComponent<Rigidbody>().position - position;
                force = distance.normalized * mass * other.mass / distance.sqrMagnitude;
                acceleration = force / mass;
                velocity += acceleration * timeSpeed;
            }
        }
    }

    public void UpdatePosition(float timeSpeed)
    {
        position += velocity * timeSpeed;
    }
}
