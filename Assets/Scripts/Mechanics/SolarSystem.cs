using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public bool simulating = false;
    public float timeSpeed = 1;
    private AstronomicalObject[] objects;

    // finding all bodies and saving their initial
    // positions in case of a reset.
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
}