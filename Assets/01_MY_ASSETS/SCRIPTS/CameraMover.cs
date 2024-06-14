using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    public GameObject camera;

    private Vector3 basePosition;

    void OnEnable(){
        GazeMasterScript.onGazeTimeUpdate += MoveCamera;
    }

    void OnDisable(){
        GazeMasterScript.onGazeTimeUpdate -= MoveCamera;
    }
    void Start(){
        basePosition = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveCamera(float gazeTime)
    {
        // Debug.Log("MoveCamera: " + gazeTime);
        transform.position = basePosition + (camera.transform.forward * gazeTime * 1.0f);
    }
}
