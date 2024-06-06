using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Hands;

public class MyHandOffset : MonoBehaviour
{   
    
    // Start is called before the first frame update
    void Start()
    {   
        XRHandSkeletonDriver driver = GetComponent<XRHandSkeletonDriver>();
        driver.ApplyRootPoseOffset(new Vector3(0.0f, 0.0f, 4.0f));
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }
}
