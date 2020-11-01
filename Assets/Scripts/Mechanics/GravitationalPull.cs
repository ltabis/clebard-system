using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalPull : MonoBehaviour
{
    public float Gravity;

    private void OnTriggerEnter(Collider body) {
        if (body.GetComponent<Player>()) {
            body.GetComponent<Player>().gravitationalPull = GetComponent<GravitationalPull>();
        }
    }
}
