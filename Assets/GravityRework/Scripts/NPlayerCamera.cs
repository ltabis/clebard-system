using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class NPlayerCamera : MonoBehaviour
{
    public float sensitivity = 1f;
    // focus on an object.
    [SerializeField]
	private Transform focus = default;
    // distance from the focused object.
	[SerializeField, Range(1f, 20f)]
	private float distance = 5f;
    // limit radius from wich the camera will start following the player. 
    [SerializeField, Min(0f)]
    private float focusRadius = 1f;
    // the speed of the camera adjusting when the player isn't moving.
    [SerializeField, Range(0f, 1f)]
    float focusCentering = .5f;
    // current point targeted by the camera.
    [SerializeField, Range(0f, 360f)]
    float rotationSpeed = 90f;
    private Vector3 focusPoint;
    // camera orientation.
    private Vector2 orbitAngles = new Vector2(45.0f, 0f);

    void ManualRotation()
    {
        Vector2 input = new Vector2(
            -Input.GetAxis("Mouse Y") * sensitivity,
             Input.GetAxis("Mouse X") * sensitivity
        );

        float threshold = 0.001f;
        if (input.x < -threshold || input.x > threshold || input.y < -threshold || input.y > threshold)
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
    }

    void LateUpdate()
    {
        ManualRotation();
        UpdateFocusPoint();
        Quaternion lookRotation = Quaternion.Euler(orbitAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;

        // relocating the camera at the desired distance from the focus point.
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    void UpdateFocusPoint()
    {
        Vector3 currentPostion = focus.position;
        if (focusRadius > 0f) {
            float dst = Vector3.Distance(currentPostion, focusPoint);
            float t = 1f;
            if (dst > 0.01f && focusCentering > 0f)
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            if (dst > focusRadius)
                t = Mathf.Min(t, focusRadius / dst);
            focusPoint = Vector3.Lerp(currentPostion, focusPoint, t);
        } else
            focusPoint = currentPostion;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
