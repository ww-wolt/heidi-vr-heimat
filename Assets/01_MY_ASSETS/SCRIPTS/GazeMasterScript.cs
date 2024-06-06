using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GazeMasterScript : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------
    // Attach this GameObject to the main camera of the XRRig prefab
    // --------------------------------------------------------------------------------------------

    public GameObject XRRigMainCamera;
    
    private void Awake()
    {
        this.transform.SetParent(XRRigMainCamera.transform);
    }

    // --------------------------------------------------------------------------------------------
    // Looking Time Script
    // --------------------------------------------------------------------------------------------

    private const int BUFFER_MAX_SECONDS = 5;

    public Action<float> onGazeTimeUpdate;

    private struct CameraOrientation
    {
        public Quaternion rotation;
        public float time;

        public CameraOrientation(Quaternion rotation, float time)
        {
            this.rotation = rotation;
            this.time = time;
        }
    }

    float lerpedLookingTime = 0.0f;
    float lookingStartTimestamp = 0.0f;
   
    // create buffer list to store all CameraOrientation objects of the last 5 seconds
    private List<CameraOrientation> cameraOrientationBuffer = new List<CameraOrientation>();
    
    void Update(){
    
        Quaternion currentRotation = transform.parent.rotation;

        float lookingTime = 0.0f;

        // Traverse Buffer backwards
        for (int i = cameraOrientationBuffer.Count - 1; i >= 0; i--)
        {
           
            // calculate the dot product between the current camera rotation and the rotation of the CameraOrientation object at index i
            float dotP = Quaternion.Dot(currentRotation, cameraOrientationBuffer[i].rotation);
            
            // if the dot product is less than 0.99, break the loop
            if (dotP < 0.995f)
            {   
                // Store starting time of looking interaction
                lookingStartTimestamp = cameraOrientationBuffer[i].time;
                // Empty Buffer up until (and including) i to prevent bug when looking back at the same location
                cameraOrientationBuffer.RemoveRange(0, i + 1);                
                break;
            }
        }

        lookingTime = Time.time - lookingStartTimestamp;
        lerpedLookingTime += (lookingTime - lerpedLookingTime) * 0.04f;

       
        // broadcast lerpedLookingTime as C# event
        onGazeTimeUpdate?.Invoke(lerpedLookingTime);
        Debug.Log("Tried to broadcast gaze time: " + lerpedLookingTime);

        // store the current camera rotation and time in the buffer list
        cameraOrientationBuffer.Add(new CameraOrientation(currentRotation, Time.time));

        // remove all CameraOrientation objects from the buffer list that are older than 5 seconds
        cameraOrientationBuffer.RemoveAll(obj => Time.time - obj.time > BUFFER_MAX_SECONDS);
    }
}
