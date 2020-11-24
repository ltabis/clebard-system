using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomGravity
{
    public static Vector3 GetGravity(Vector3 position)
    {
        return position.normalized * Physics.gravity.y;
    }

    public static Vector3 GetGravity(Vector3 position, out Vector3 worldUp)
    {
        worldUp = GetWorldUp(position);
        return GetGravity(position);
    }

    public static Vector3 GetWorldUp(Vector3 position)
    {
        return position.normalized;
    }
}