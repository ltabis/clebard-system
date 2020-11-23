using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class NPlayerCamera : MonoBehaviour
{
    // focus on an object.
    [SerializeField]
	Transform focus = default;
    // distance from the focused object.
	[SerializeField, Range(1f, 20f)]
	float distance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        // relocating the camera at the desired distance from the focus point.
        transform.localPosition = focus.position - transform.forward * distance;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
