using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Universe
{
    public const float gravitationnalConstant = 0.0001f;
}

public class SolarSystem : MonoBehaviour
{
    public bool simulating = false;
    public float timeSpeed = 1;
    private AstronomicalObject[] objects;

    private void Awake()
    {
        objects = FindObjectsOfType<AstronomicalObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (simulating) {
            UpdateAllVeclocities();
            UpdateAllPositions();
        }
    }

    void UpdateAllVeclocities()
    {
        foreach (var obj in objects)
            obj.UpdateVelocity(objects, timeSpeed);
    }

    void UpdateAllPositions()
    {
        foreach (var obj in objects)
            obj.UpdatePosition(timeSpeed);
    }
}