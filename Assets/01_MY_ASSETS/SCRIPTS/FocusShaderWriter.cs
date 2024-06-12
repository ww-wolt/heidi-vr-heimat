using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusShaderWriter : MonoBehaviour
{
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;

    }

    void Update()
    {
        
    }

    public void WriteVignette(float strength)
    {
        // mat.SetFloat("_FresnelStrength", strength);
        mat.SetFloat("_AlphaStrength", strength);
    }
}
