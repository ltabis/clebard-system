using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    bool found = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            found = true;
    }

    public bool HasBeenFound()
    {
        return found;
    }
}
