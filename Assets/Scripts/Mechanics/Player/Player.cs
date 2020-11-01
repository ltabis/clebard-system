using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GravitationalPull gravitationalPull;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    private void FixedUpdate() {
        if (gravitationalPull) {
            Vector3 gravityUp = (transform.position - gravitationalPull.transform.position).normalized;
            Vector3 localUp = transform.up;

            rb.AddForce(gravityUp * gravitationalPull.Gravity);
            rb.rotation = Quaternion.FromToRotation(localUp, gravityUp) * rb.rotation;
            //rb.AddForce((gravitationalPull.transform.position - transform.position).normalized * gravitationalPull.Gravity);
        }
    }
}