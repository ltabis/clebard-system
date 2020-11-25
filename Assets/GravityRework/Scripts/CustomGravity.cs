using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomGravity
{
    public static List<GravitySource> sources = new List<GravitySource>();

    public static Vector3 GetGravity(Vector3 position)
    {
        Vector3 gravitySum = Vector3.zero;

        // accumulating all gravities in the scene.
        foreach (var gravity in sources)
            gravitySum += gravity.GetGravity(position);

        return gravitySum;
    }

    public static Vector3 GetGravity(Vector3 position, out Vector3 worldUp)
    {
        Vector3 gravitySum = GetGravity(position);

        worldUp = -gravitySum.normalized;
        return gravitySum;
    }

    public static Vector3 GetWorldUp(Vector3 position)
    {
        return -GetGravity(position).normalized;
    }

    public static void RegisterGravitySource(GravitySource source)
    {
        Debug.Assert(
            !sources.Contains(source),
            "Trying to add a gravity source already registered.",
            source
        );
        sources.Add(source);
    }
    public static void UnregisterGravitySource(GravitySource source)
    {
        Debug.Assert(
            sources.Contains(source),
            "Trying to remove a gravity source that was not registered.",
            source
        );
        sources.Remove(source);
    }

    public static void ChangeGravitySourceState(uint index, bool status)
    {
        if (index < sources.Count)
            sources[(int)index].isEnabled = status;
    }
}