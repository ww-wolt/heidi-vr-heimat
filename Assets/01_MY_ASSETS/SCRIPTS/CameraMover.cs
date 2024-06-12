using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    public GameObject camera;

    private Vector3 basePosition;
    void Start(){
        basePosition = transform.position;
        GazeMasterScript.onGazeTimeUpdate += MoveCamera;
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
