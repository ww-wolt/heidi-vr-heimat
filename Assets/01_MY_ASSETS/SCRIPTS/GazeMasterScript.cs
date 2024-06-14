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


    private const float DOT_P_THRESHOLD = 0.995f;
    private const float DOT_P_THRESHOLD_CONNECTED = 0.9f;

    private float dotPThreshold = DOT_P_THRESHOLD;

    
    private void Awake()
    {
        this.transform.SetParent(XRRigMainCamera.transform);
    }

    // --------------------------------------------------------------------------------------------
    // Looking Time Script
    // --------------------------------------------------------------------------------------------

    private const int BUFFER_MAX_SECONDS = 5;

    public static Action<float> onGazeTimeUpdate;

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

    
    private Vector3 cameraBasePosition;
    void Start(){
        cameraBasePosition = transform.parent.position;
        EntityController.onConnectionStateUpdate += updateAngleThreshold;
    }

    void updateAngleThreshold(bool connection)
    {
        if (connection)
        {
            dotPThreshold = DOT_P_THRESHOLD_CONNECTED;
        }
        else
        {
            dotPThreshold = DOT_P_THRESHOLD;
        }
    }
    
    void Update(){
    
        Quaternion currentRotation = transform.parent.rotation;

        float lookingTime = 0.0f;

        // Traverse Buffer backwards
        for (int i = cameraOrientationBuffer.Count - 1; i >= 0; i--)
        {
           
            // calculate the dot product between the current camera rotation and the rotation of the CameraOrientation object at index i
            float dotP = Quaternion.Dot(currentRotation, cameraOrientationBuffer[i].rotation);
            
            // if the dot product is less than 0.99, break the loop
            if (dotP < dotPThreshold)
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
        // Debug.Log("Broadcast gaze time: " + lerpedLookingTime);

        // store the current camera rotation and time in the buffer list
        cameraOrientationBuffer.Add(new CameraOrientation(currentRotation, Time.time));

        // remove all CameraOrientation objects from the buffer list that are older than 5 seconds
        cameraOrientationBuffer.RemoveAll(obj => Time.time - obj.time > BUFFER_MAX_SECONDS);


        // --------------------------------------------------------------------------------------------
        // Raycast
        // --------------------------------------------------------------------------------------------
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

        // get the hit which angle is closest to the camera forward direction
        RaycastHit closestHit = new RaycastHit();
        float minAngle = 180.0f;
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            float angle = Vector3.Angle(transform.forward, hit.transform.position - transform.position);
            if (angle < minAngle)
            {
                minAngle = angle;
                closestHit = hit;
            }
        }

        // closesHit transform not equaly null
        if (closestHit.transform != null)
        {
            // Debug.Log("Closest hit: " + closestHit.transform.GetInstanceID());

            // log the index of the gameobject inside its parent
            // Debug.Log("Sibling Index: " + closestHit.transform.GetSiblingIndex());

            // if closest hit has tag "Entity"
            if (closestHit.transform.tag == "Entity"){
                // get the EntityController script of the closest hit
                EntityControllerOld entityController = closestHit.transform.GetComponent<EntityControllerOld>();
                if (entityController != null)
                {
                    // increase focus level of the entity
                    entityController.focusLevel += Time.deltaTime * 16.0f;
                }
                // entityController.focusLevel += Time.deltaTime;
            }
        }
    }
}
