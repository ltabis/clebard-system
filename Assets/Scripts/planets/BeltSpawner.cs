using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltSpawner : MonoBehaviour
{
    [Header("Spawner settings")]
    public float innerRadius;
    public float outerRadius;
    public GameObject astroidPrefab;
    public uint astroidDensity;
    public int seed;
    public float height;
    public Vector3 orbitAngle;

    [Header("Astroïds settings")]
    public float minRotationSpeed;
    public float maxRotationSpeed;
    public float orbitSpeed;
    public Vector3 scale;

    private float randomRadius;
    private float randomPositionOnDisk;
    private Vector3 localPosition;
    private Vector3 worldPosition;
    private Quaternion randomRotation;

    void Start()
    {
        // prevent bad repartition of asteroïds.
        Random.InitState(seed);
        transform.rotation = Quaternion.Euler(orbitAngle);

        for (uint i = 0; i < astroidDensity; ++i) {
            randomRadius = Random.Range(innerRadius, outerRadius);
            randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            randomPositionOnDisk = Random.Range(0, 2 * Mathf.PI);

            // computing the asteroid's position in the world.
            localPosition.y = Random.Range(-height, height);
            localPosition.x = randomRadius * Mathf.Cos(randomPositionOnDisk);
            localPosition.z = randomRadius * Mathf.Sin(randomPositionOnDisk);
            worldPosition = transform.rotation * localPosition + transform.position;

            GameObject asteroid = Instantiate(
                astroidPrefab,
                worldPosition,
                randomRotation
            );

            // adding a rotation script to the asteroid.
            asteroid.AddComponent<RotatingObject>();
            asteroid.AddComponent<RotatingAroundObject>();
            RotatingObject rotatingC = asteroid.GetComponent<RotatingObject>();
            RotatingAroundObject aroundC = asteroid.GetComponent<RotatingAroundObject>();

            // setting up components.
            rotatingC.raySize = 0.0f;
            rotatingC.rotateSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            rotatingC.rotationAxis = randomRotation.eulerAngles;
            aroundC.target = gameObject;
            aroundC.speed = orbitSpeed;

            asteroid.transform.localScale = scale;
            asteroid.transform.parent = gameObject.transform;
        }

    }
}
