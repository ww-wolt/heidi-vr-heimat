using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyController : MonoBehaviour
{
    public GameObject rotatingObject;
    public float distance = 5.0f;

    void Update()
    {
        // Update the position of the rotating object to maintain the specified distance
        if (rotatingObject != null)
        {
            Vector3 direction = (rotatingObject.transform.position - transform.position).normalized;
            rotatingObject.transform.position = transform.position + direction * distance;
        }
    }

   
    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
