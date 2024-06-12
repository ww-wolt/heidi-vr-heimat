using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 10.0f;

    void Update()
    {
        if (target != null)
        {
            // Rotate around the target (empty GameObject)
            transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
