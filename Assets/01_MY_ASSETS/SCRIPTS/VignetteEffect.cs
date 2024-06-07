using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffect : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {   
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        vignette.intensity.value = 1.0f;
        // get vignette component from postprocessingvolume gameobject
        // postProcessingVolume.GetComponent<PostProcessVolume>().profile.GetSetting<PostProcessing.Vignette>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // listen for event
    // void OnEnable()
    // {
    //     EventManager.OnGhostSpawned += VignetteEffectOn;
    // }
}
