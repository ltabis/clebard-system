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
    public AstronomicalObject relativeBody;

    private Vector3 relativeBodyInitialPosition = Vector3.zero;
    private int relativeBodyIndex = -1;
    private AstronomicalObject[] allBodies;
    private DebugBody[] allDebugBodies;

    private void Initialize()
    {
        allBodies = FindObjectsOfType<AstronomicalObject>();
        allDebugBodies = new DebugBody[allBodies.Length];

        for (uint i = 0; i < allBodies.Length; ++i)
            allDebugBodies[i] = new DebugBody(allBodies[i]);
    }

    private void GetRelativeBodyData()
    {
        if (relativeBody) {
            for (int i = 0; i < allBodies.Length; ++i)
                if (relativeBody == allBodies[i]) {
                    relativeBodyIndex = i;
                    relativeBodyInitialPosition = allDebugBodies[i].position;
                    return;
                }
        } else {
            relativeBodyIndex = -1;
            relativeBodyInitialPosition = Vector3.zero;
        }
    }

    private void Update()
    {
        // Simulating orbits from all celestial bodies.
        if (debug && !Application.isPlaying) {

            Initialize();
            GetRelativeBodyData();

            // creating a list of movements for each iterations and each bodies.
            Vector3[][] orbitsPositions = new Vector3[allDebugBodies.Length][];
            for (uint body = 0; body < allDebugBodies.Length; ++body)
                orbitsPositions[body] = new Vector3[iterations];

            // simulating n times for each bodies.
            for (uint i = 0; i < iterations; ++i)
                Simulate(orbitsPositions, i);

            // displaying all orbits (relative or not to a body).
            DrawOrbits(orbitsPositions);
        }
    }

    private void DrawOrbits(Vector3[][] orbitsPositions)
    {
        for (uint body = 0; body < allDebugBodies.Length; ++body)
            for (uint i = 0; i < iterations - 1; ++i)
                Debug.DrawLine(
                    orbitsPositions[body][i],
                    orbitsPositions[body][i + 1],
                    allDebugBodies[body].orbitColor,
                    0.1f
                );
    }
    // simulating the orbit of the body.
    private void Simulate(Vector3[][] orbitsPositions, uint iteration)
    {
        // updating velocities.
        foreach (var body in allDebugBodies)
            foreach (var other in allDebugBodies)
                UpdateDebugBodyVelocity(body, other);

        // updating position and drawing orbits.
        for (uint i = 0; i < allDebugBodies.Length; ++i)
            UpdateDebugBodyPosition(allDebugBodies[i], orbitsPositions[i], iteration);
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

    public void UpdateDebugBodyPosition(DebugBody body, Vector3[] currentOrbitPositions, uint iteration)
    {
        Vector3 previousPosition = body.position;
        Vector3 newPosition = body.position + body.velocity * timeSpeed;

        body.position = newPosition;

        // When drawing debug lines we can compare
        // orbits from all objects or relative to only one body.
        if (relativeBody && relativeBody != body.reference)
            newPosition -= allDebugBodies[relativeBodyIndex].position - relativeBodyInitialPosition;
        else if (relativeBody == body.reference)
            newPosition = relativeBodyInitialPosition;

        // updating the next point to draw.
        currentOrbitPositions[iteration] = newPosition;
    }
}