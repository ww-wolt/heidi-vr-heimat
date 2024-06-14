using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator doomAnimator;

    public Animator fallingAnimator;

    public AudioSource heimatBackgroundAudio;
    public AudioSource crumblingAudio;

    public GameObject fallingParticles;

    
    void Start()
    {

    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ConnectionLine")
        {
            crumblingAudio.Play();
            doomAnimator.enabled = true;
            
            
            StartCoroutine(AudioFader.StartFade(heimatBackgroundAudio, 2.0f, 0.0f));
            
            // get the particle system component of the falling particles game object
            ParticleSystem particleSystem = fallingParticles.GetComponent<ParticleSystem>(); 
            particleSystem.Play();
            fallingAnimator.enabled = true;

            // disable my collider
            GetComponent<BoxCollider>().enabled = false;
            // gameObject.SetActive(false);
        }
    }
}
