using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSizeWriter : MonoBehaviour
{
    void WriteScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
