using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitVizualiser : MonoBehaviour
{
    public Color orbitColor = Color.white;
    public bool debug = true;
    public float timeSpeed = 1;
    public uint iterations = 100;

    private Vector3 velocity;
    private Vector3 position;
    private Vector3 initialPosition;
    private AstronomicalObject reference;
    private AstronomicalObject[] allBodies;

    private void Start()
    {
        allBodies = FindObjectsOfType<AstronomicalObject>();
        reference = GetComponent<AstronomicalObject>();

        velocity = reference.initialVelocity;
        position = reference.Position;
        initialPosition = reference.Position;
    }

    private void Update()
    {
        if (debug)
            Simulate();
    }

    // simulating the orbit of the body.
    private void Simulate()
    {
        Vector3 currentPosition = reference.Position,
                previousPosition = reference.Position;

        velocity = reference.initialVelocity;
        position = reference.Position;

        for (uint i = 0; i < iterations; ++i) {
            UpdateVelocity(allBodies, timeSpeed);
            UpdatePosition(timeSpeed);

            currentPosition = position;
            Debug.DrawLine(previousPosition, currentPosition, orbitColor, 0.01f);
            previousPosition = currentPosition;
        }
    }

    // update a preview of the velocity of the
    // referenced body.
    public void UpdateVelocity(AstronomicalObject[] objects, float timeSpeed)
    {
        // no need to redeclare all variables for each loop.
        Vector3 distance, force, acceleration;

        foreach (var other in objects) {
            if (other != reference) {
                // calculating the force to get the current velocity of the body.
                // G(m1m2/r2)
                distance = other.Position - position;
                force = distance.normalized * reference.mass * other.mass / distance.sqrMagnitude;
                acceleration = force / reference.mass;
                velocity += acceleration * timeSpeed;
            }
        }
    }

    // update a preview of the position of the
    // referenced body.
    public void UpdatePosition(float timeSpeed)
    {
        position += velocity * timeSpeed;
    }
}