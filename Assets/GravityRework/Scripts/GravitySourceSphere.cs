﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySourceSphere : GravitySource
{
    // gravity force.
    public float gravity = 9.81f;
    // gravity range.
    [SerializeField, Min(0f)]
    private float gravityRadius = 10f, gravityFallOffRadius = 15f;

    void Awake()
    {
        OnValidate();
    }

    void OnValidate()
    {
        gravityFallOffRadius = Mathf.Max(gravityFallOffRadius, gravityRadius);
    }

    public override Vector3 GetGravity(Vector3 position)
    {
        Vector3 vector = transform.position - position;
        float distance = vector.magnitude;

        if (distance > gravityFallOffRadius)
            return Vector3.zero;

        float g = gravity / distance;

        if (distance > gravityRadius)
            g *= 1f - (distance - gravityRadius) * (1f / (gravityFallOffRadius - gravityRadius));

        return g * vector;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gravityFallOffRadius);
    }
}
