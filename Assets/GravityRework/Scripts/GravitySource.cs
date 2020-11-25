using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySource : MonoBehaviour
{
    public bool isEnabled = true;

    public virtual Vector3 GetGravity(Vector3 position)
    {
        return Physics.gravity;
    }

    void OnEnable()
    {
        CustomGravity.RegisterGravitySource(this);
    }

    void OnDisable() 
    {
        CustomGravity.UnregisterGravitySource(this);
    }
}
