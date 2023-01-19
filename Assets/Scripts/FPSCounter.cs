using System;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private float deltaTime;

    private TextMeshProUGUI fpsText;

    private void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        var fps = 1.0f / deltaTime;
        fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
    }
}