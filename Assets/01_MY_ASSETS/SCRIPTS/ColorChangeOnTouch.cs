using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOnTouch : MonoBehaviour




{
    public Material emissiveMaterial;
    public Material opaqueMaterial;



    private void OnTriggerEnter(Collider other)


    {


        GetComponent<Renderer>().material = emissiveMaterial;
    }

    private void OnTriggerExit(Collider other)

    {
        GetComponent<Renderer>().material = opaqueMaterial;
    }

}
