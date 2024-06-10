using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleEffect : MonoBehaviour
{
    public float wiggleAmount = 0.1f; // Adjust this value to control the intensity of the wiggle
    public float wiggleSpeed = 5f; // Adjust this value to control the speed of the wiggle

    public float smoothTime = 0.1f;

    private Vector3 originalPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        originalPosition = transform.position;

    }

    void Update()
    {
        // Calculate the target position using Perlin noise
        float xNoise = Mathf.PerlinNoise(Time.time * wiggleSpeed, 0) * 2 - 1;
        float yNoise = Mathf.PerlinNoise(0, Time.time * wiggleSpeed) * 2 - 1;
        Vector3 targetPosition = originalPosition + new Vector3(xNoise, yNoise, 0) * wiggleAmount;

        // Smoothly move towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
