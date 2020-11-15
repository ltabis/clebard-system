using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventShadowCasting : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        GetComponent<Renderer>().receiveShadows = false;
    }
}
