using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SolarSystem : MonoBehaviour
{
    public bool simulating = false;
    public bool debugging = true;
    public float timeSpeed = 1;
    private AstronomicalObject[] objects;
    private Vector3[] initialObjectPosition;

    // finding all bodies and saving their initial
    // positions in case of a reset.
    private void Awake()
    {
        objects = FindObjectsOfType<AstronomicalObject>();
        initialObjectPosition = new Vector3[objects.Length];

        for (uint i = 0; i < objects.Length; ++i)
            if (objects[i].IsInitialized)
                initialObjectPosition[i] = objects[i].Position;
    }

    // Update is called once per frame
    void Update()
    {
        if (simulating) {
            UpdateAllVeclocities();
            UpdateAllPositions();
        }

        DebugAllOrbits();
    }

    void DebugAllOrbits() {
        for (uint i = 0; i < objects.Length; ++i)
            if (objects[i].IsInitialized) {
                OrbitVizualiser vizualiser = objects[i].GetComponent<OrbitVizualiser>();

                if (vizualiser) {
                    vizualiser.debug = debugging;
                }
            }
    }

    // iterating trought all bodies
    // and updating their velocity.
    void UpdateAllVeclocities()
    {
        foreach (var obj in objects)
            obj.UpdateVelocity(objects, timeSpeed);
    }

    // iterating trought all bodies
    // and updating their position.
    void UpdateAllPositions()
    {
        foreach (var obj in objects)
            obj.UpdatePosition(timeSpeed);
    }

    // resets the position of all bodies.
    public void ResetPositions()
    {
        for (uint i = 0; i < objects.Length; ++i)
            objects[i].SetPosition(initialObjectPosition[i]);
    }
}

// custom editor to add a reset button.
[CustomEditor(typeof(SolarSystem))]
public class SolarSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SolarSystem myTarget = (SolarSystem)target;

        if(GUILayout.Button("Reset universe"))
        {
            myTarget.ResetPositions();
        }
    }
} 