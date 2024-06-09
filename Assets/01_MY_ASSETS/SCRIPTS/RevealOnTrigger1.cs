using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealOnTrigger1 : MonoBehaviour
{
    public Transform playerHead;
    public float maxAngle = 45.0f; // Maximum angle for horizontal high pass effect
    public float minAngle = -45.0f; // Minimum angle for horizontal high pass effect
    public float maxVerticalAngle = 45.0f; // Maximum angle for vertical high pass effect
    public float minVerticalAngle = -45.0f; // Minimum angle for vertical high pass effect
    public float maxCutoffFrequency = 800f; // Cutoff frequency for high pass filter



    public AudioHighPassFilter highPassFilter;
    public AudioSource audioSource;

    private bool isPlayerInTrigger = false;

    void Start()
    {
        highPassFilter = GetComponent<AudioHighPassFilter>();
        audioSource = GetComponent<AudioSource>();

        if (highPassFilter == null || audioSource == null)
        {
            enabled = false;
        }
    }
    void Update()
    {
        if (isPlayerInTrigger)
        {
            // Player is in the trigger area, disable the high pass filter
            highPassFilter.enabled = false;
        }
        else
        {
            // Calculate the angle between XR rig's forward direction and object's left direction
            float angleLeft = Vector3.Angle(playerHead.forward, -transform.right);

            // Calculate the angle between XR rig's forward direction and object's right direction
            float angleRight = Vector3.Angle(playerHead.forward, transform.right);

            // Calculate the minimum of the two horizontal angles
            float minAngleLR = Mathf.Min(angleLeft, angleRight);

            // Calculate the angle between XR rig's forward direction and object's upward direction
            float angleUp = Vector3.Angle(playerHead.forward, transform.up);

            // Calculate the angle between XR rig's forward direction and object's downward direction
            float angleDown = Vector3.Angle(playerHead.forward, -transform.up);

            // Calculate the minimum of the two vertical angles
            float minAngleUD = Mathf.Min(angleUp, angleDown);

            // Calculate the overall minimum angle (combining horizontal and vertical)
            float minAngleOverall = Mathf.Min(minAngleLR, minAngleUD);

            // Smoothly transition cutoff frequency based on the minimum overall angle
            float cutoffFrequency = Mathf.Lerp(maxCutoffFrequency, 0, Mathf.Clamp01((minAngleOverall - minAngle) / (maxAngle - minAngle)));

            // Apply high pass effect
            highPassFilter.cutoffFrequency = cutoffFrequency;
            highPassFilter.enabled = true;

            // Determine if the audio source should be enabled
            bool isWithinHorizontalRange = minAngleLR >= Mathf.Min(minAngle, maxAngle) && minAngleLR <= Mathf.Max(minAngle, maxAngle);
            bool isWithinVerticalRange = minAngleUD >= Mathf.Min(minVerticalAngle, maxVerticalAngle) && minAngleUD <= Mathf.Max(minVerticalAngle, maxVerticalAngle);
            bool isWithinAnyRange = isWithinHorizontalRange || isWithinVerticalRange;

            // Activate audio source if the player is looking towards any of the cones
            audioSource.enabled = minAngleOverall >= Mathf.Min(minAngle, minVerticalAngle);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerInTrigger = false;
    }
}
