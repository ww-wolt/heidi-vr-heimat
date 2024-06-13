using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityControllerOld : MonoBehaviour
{
    private GameObject camera;
    public float focusLevel = 0.0f;

    private float lerpedFocusLevel = 0.0f;

    private Vector3 basePosition;

    void Start()
    {
        basePosition = transform.position;
        camera = GameObject.Find("Main Camera");
    }
    void Update()
    {
        if(focusLevel > 0.0f)
        {
            focusLevel -= Time.deltaTime * 8.0f;
            // Debug.Log("Focus Level: " + focusLevel);
        }

        lerpedFocusLevel = lerpedFocusLevel + (focusLevel - lerpedFocusLevel) * Time.deltaTime * 0.4f;

        // transform myself according to focus level
        float scale = 0.3f + lerpedFocusLevel * 0.1f;
        transform.localScale = new Vector3(scale, scale, scale);

        // move myself closer to camera according to focus level
        transform.position = basePosition + (camera.transform.position - basePosition) * lerpedFocusLevel * 0.1f;
    }
}
