using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAroundObject : MonoBehaviour
{
    public GameObject target;
    public float speed;

    void Update()
    {
        transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
    }
}
