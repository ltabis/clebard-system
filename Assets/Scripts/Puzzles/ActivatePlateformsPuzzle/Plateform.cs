using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : MonoBehaviour
{
    [SerializeField]
    private Light lightSource;
    private bool isActivated = false;
    private bool isDisabled = true;

    void Awake()
    {
        lightSource.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDisabled)
            return;

        if (other.tag == "Player") {
            isActivated = !isActivated;
            if (isActivated)
                lightSource.color = Color.green;
            else
                lightSource.color = Color.red;
        }
    }

    public void EnablePlateform()
    {
        lightSource.enabled = true;
        isDisabled = false;
        lightSource.color = Color.red;
    }

    public bool IsActivated {
        get {
            return isActivated;
        }
    }
}
