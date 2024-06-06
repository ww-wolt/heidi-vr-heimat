using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurDistortionEffect : MonoBehaviour
{
    public GameObject XRRigMainCamera;
    
    private void Awake()
    {
        this.transform.SetParent(XRRigMainCamera.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
