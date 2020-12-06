using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ColorBlink : MonoBehaviour
{
    TMP_Text text;
    public float speed = 1f;
    float elapsed = 0f;
    public float min = 0f;
    public float max = 1f;
    private bool increasing = true;
    Color color;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        color = text.color;
        speed *= 0.1f;
    }

    void Update()
    {
        if (color.r <= max && increasing)
        {
            color.r += speed * Time.deltaTime;
            color.g += speed * Time.deltaTime;
            color.b += speed * Time.deltaTime;
            //color.a += (speed / 2) * Time.deltaTime;
        }
        if (color.r >= min && !increasing)
        {
            color.r -= speed * Time.deltaTime;
            color.g -= speed * Time.deltaTime;
            color.b -= speed * Time.deltaTime;
            //color.a -= (speed / 2) * Time.deltaTime;
        }

        if (color.r > max && increasing)
            increasing = false;
        else if (color.r < min && !increasing)
            increasing = true;

        text.color = color;

    }
}
