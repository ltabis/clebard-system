using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBody {
    public AstronomicalObject reference;
    public Color orbitColor;
    public Vector3 position;
    public Vector3 velocity;
    public float mass;
    public string name;
    public DebugBody(AstronomicalObject reference)
    {
        this.reference = reference;
        this.orbitColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        this.position = reference.transform.position;
        this.velocity = reference.initialVelocity;
        this.mass = reference.mass;
        this.name = reference.name;
    }

    public void Reininitialize()
    {
        if (!reference)
            return;
        position = reference.transform.position;
        velocity = reference.initialVelocity;
        mass = reference.mass;
        name = reference.name;
    }
}

[ExecuteInEditMode] public class OrbitVizualiser : MonoBehaviour
{
    public bool debug = true;
    public float timeSpeed = 1;
    public uint iterations = 100;
    public AstronomicalObject solarSystemCenter;

    private AstronomicalObject[] allBodies;
    private List<DebugBody> allDebugBodies;

    private void Initialize()
    {
        if (!Application.isPlaying) {
            allBodies = FindObjectsOfType<AstronomicalObject>();
            allDebugBodies = new List<DebugBody>(allBodies.Length);

            foreach (var body in allBodies)
                allDebugBodies.Add(new DebugBody(body));
        }
    }
    private void Update()
    {
        if (allDebugBodies == null)
            Initialize();
        // Simulating orbits from all celestial bodies.
        if (debug && !Application.isPlaying) {

            allBodies = FindObjectsOfType<AstronomicalObject>();

            if (allBodies.Length > allDebugBodies.Count)
                AddDebugBodies();
            else if (allBodies.Length < allDebugBodies.Count)
                removeDebugBodies();
            for (uint i = 0; i < iterations; ++i)
                Simulate();

            // reinitializing debug bodies with their reference
            // in case changes were made.
            ReInitilializeDebugBodies();
        }
    }

    private void ReInitilializeDebugBodies()
    {
        foreach (var debugBody in allDebugBodies)
            debugBody.Reininitialize();
    }

    private void AddDebugBodies()
    {
        for (int i = 0; i < allBodies.Length; ++i)
            if (allDebugBodies.Find(debugBody => debugBody.name == allBodies[i].name) == null)
                allDebugBodies.Add(new DebugBody(allBodies[i]));
    }

    private void removeDebugBodies()
    {
        allDebugBodies.RemoveAll(debugBody => !debugBody.reference);
    }

    // simulating the orbit of the body.
    private void Simulate()
    {
        // updating velocities.
        foreach (var body in allDebugBodies)
            foreach (var other in allDebugBodies)
                UpdateDebugBodyVelocity(body, other);

        Vector3 previousPosition;

        // updating position and drawing orbits.
        foreach (var body in allDebugBodies) {
            previousPosition = body.position;
            UpdateDebugBodyPosition(body);
            Debug.DrawLine(previousPosition, body.position, body.orbitColor, 0.01f);
        }
    }

    public void UpdateDebugBodyVelocity(DebugBody body, DebugBody other)
    {
        if (body == other)
            return;

        // calculating the force to get the current velocity of the body.
        // G(m1m2/r2)
        Vector3 distance = other.position - body.position;
        Vector3 acceleration = Universe.GravitationalConstant * distance.normalized * other.mass / distance.sqrMagnitude;

        body.velocity += acceleration * timeSpeed;
    }

    public void UpdateDebugBodyPosition(DebugBody body)
    {
        body.position += body.velocity * timeSpeed;
    }
}