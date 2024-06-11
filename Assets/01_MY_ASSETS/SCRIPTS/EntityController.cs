using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public float focusLevel = 0.0f;

    private float lerpedFocusLevel = 0.0f;

    // Update is called once per frame
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
        transform.localScale = new Vector3(scale, scale, scale );
    }
}
