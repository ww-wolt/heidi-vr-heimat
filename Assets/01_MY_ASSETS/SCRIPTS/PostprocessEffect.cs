using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffect : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;
    private DepthOfField depthOfField;

    // Start is called before the first frame update
    void Start()
    {   
        volume = GetComponent<Volume>();
        
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out depthOfField);
        // vignette.intensity.value = 1.0f;

        GazeMasterScript.onGazeTimeUpdate += UpdateVignette;
        GazeMasterScript.onGazeTimeUpdate += UpdateFocus;

    }

    void UpdateVignette(float gazeTime)
    {
     
        vignette.intensity.value = gazeTime * 1.0f;
    }

    void UpdateFocus(float gazeTime)
    {
        // depthOfField.focusDistance.value = gazeTime * 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
