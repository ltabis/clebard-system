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
    [SerializeField, Min(0f)]
    float focusRadius = 1f;

    Vector3 focusPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        UpdateFocusPoint();
        // relocating the camera at the desired distance from the focus point.
        transform.localPosition = focusPoint - transform.forward * distance;    
    }

    void UpdateFocusPoint()
    {
        Vector3 currentPostion = focus.position;
        if (focusRadius > 0f) {
            float dst = Vector3.Distance(currentPostion, focusPoint);
            if (dst > focusRadius)
                focusPoint = Vector3.Lerp(currentPostion, focusPoint, focusRadius / dst);
        } else
            focusPoint = currentPostion;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
