using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingLight : MonoBehaviour
{
    [SerializeField]
    private float pulseRange = 4f;
    [SerializeField]
    private float pulseSpeed = 3f;
    [SerializeField]
    private float pulseMinimum = 0f;

    private Light lightSource;

    void Start()
    {
        lightSource = GetComponent<Light>();
    }

    void Update()
    {
        lightSource.range = pulseMinimum + Mathf.PingPong(Time.time * pulseSpeed, pulseRange - pulseMinimum);
    }
}
