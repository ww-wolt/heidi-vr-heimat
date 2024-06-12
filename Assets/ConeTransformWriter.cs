using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeTransformWriter : MonoBehaviour
{


    public void WriteZPosition(float zPos)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}
