using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class TransformWriter : MonoBehaviour
{
   public void writeXPosition(float x)
   {
       transform.position = new Vector3(x, transform.position.y, transform.position.z);
   }

    public void writeYPosition(float y)
    {
         transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public void writeZPosition(float z)
    {
         transform.position = new Vector3(transform.position.x, transform.position.y, z);
         
    }

    public void writeLocalXPosition(float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    public void writeLocalYPosition(float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    public void writeLocalZPosition(float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }

    public void writeXRotation(float x)
    {
        transform.rotation = Quaternion.Euler(x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public void writeYRotation(float y)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
    }

    public void writeZRotation(float z)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, z);
    }

    public void writeXScale(float x)
    {
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    public void writeYScale(float y)
    {
        transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
    }

    public void writeZScale(float z)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
    }
}
