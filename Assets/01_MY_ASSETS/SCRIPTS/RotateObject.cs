using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    
    public float rotationSpeedY = 30f;

    
    void Update()
    {
        
        float rotationY = rotationSpeedY * Time.deltaTime;

        transform.Rotate(0, rotationY, 0);
    }
}
