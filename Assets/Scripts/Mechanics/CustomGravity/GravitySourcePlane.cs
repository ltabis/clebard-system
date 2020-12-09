using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySourcePlane : GravitySource
{
    // gravity force.
    public float gravity = 9.81f;
    public bool debug = true;
    // gravity range.
    [SerializeField, Min(0f)]
    public float range = 1f;

    public override Vector3 GetGravity(Vector3 position)
    {
        float distance = Vector3.Dot(transform.up, position - transform.position);

        if (distance > range)
            return Vector3.zero;

        float g = -gravity;
        if (distance > 0f)
            g *= 1f - distance / range;

        return g * transform.up;
    }

    void OnDrawGizmos()
    {
        if (!debug)
            return;

        Vector3 GSize = new Vector3(1.0f, 0.0f, 1.0f);
        Vector3 scale = transform.localScale;

        scale.y = range;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, scale);
        Gizmos.color  = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, GSize);
        if (range > 0f) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(Vector3.up, GSize);
        }
    }
}
