using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    private bool hasBeenEntered = false; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") hasBeenEntered = true;
    }

    public void ResetArea()
    {
        hasBeenEntered = false;
    }

    public bool HasBeenEntered {
        get {
            return hasBeenEntered;
        }
    }
}
