﻿using System.Collections;
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

    [SerializeField, Range(0f, 360f)]
    float minVerticalAngle = -30f, maxVerticalAngle = 45f;
    private Vector3 focusPoint;
    // camera orientation.
    private Vector2 orbitAngles = new Vector2(45.0f, 0f);

    void Awake()
    {
        focusPoint = focus.position;
        transform.localRotation = Quaternion.Euler(orbitAngles);
    }

    // called only once when the script is first launch.
    void OnValidate()
    {
        // the maximum vertical angle must never be less that the minimum.
        // we only need to check this condition once, so we put that in OnValidate.
        if (maxVerticalAngle < minVerticalAngle)
            maxVerticalAngle = minVerticalAngle;
    }

    // main update loop.
    void LateUpdate()
    {
        Quaternion lookRotation;
        // we only need to clamp angles if inputs were received.
        if (ManualRotation()) {
            clampAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        } else
            lookRotation = transform.localRotation;
    
        UpdateFocusPoint();
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;

        // relocating the camera at the desired distance from the focus point.
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    // rotate the camera following player's input.
    bool ManualRotation()
    {
        Vector2 input = new Vector2(
            -Input.GetAxis("Mouse Y") * sensitivity,
             Input.GetAxis("Mouse X") * sensitivity
        );

        float threshold = 0.001f;
        if (input.x < -threshold || input.x > threshold || input.y < -threshold || input.y > threshold) {
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            return true;
        }
        return false;
    }

    void clampAngles()
    {
        // clamping vertical angle to never be bigger than maxVerticalAngle or smaller than minVerticalAngle.
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

        // clamping horizontal value to prevent going into crasy numbers.
        if (orbitAngles.y < 0)
            orbitAngles.y += 360f;
        else if (orbitAngles.y >= 360)
            orbitAngles.y -= 360f;
    }

    // update the focus point of the camera reach frame.
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
}
