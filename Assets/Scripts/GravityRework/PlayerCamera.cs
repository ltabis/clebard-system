using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    // object to focus.
    [SerializeField]
    private Transform focus;
    // distance from the object.
    [SerializeField, Range(1f, 20f)]
    private float distance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
