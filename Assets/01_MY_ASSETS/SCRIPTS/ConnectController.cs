using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // trigger enter
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.name);
    }
}
