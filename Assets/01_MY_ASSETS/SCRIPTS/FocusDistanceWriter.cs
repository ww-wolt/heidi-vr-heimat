using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class FocusDistanceWriter : MonoBehaviour
{

     private DepthOfField depthOfField;

     void Start()
     {  
        Volume volume = GetComponent<Volume>();
        volume.profile.TryGet(out depthOfField);
     }
    public void WriteFocusDistance(float focusDistance)
    {
        depthOfField.focusDistance.value = focusDistance;
    }
}
