using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntitiesStarter : MonoBehaviour
{
    public AudioSource fallingAudio;
    public GameObject entitySpawner;
    public AudioSource entityBackgroundAudio;

    public void startEntities()
    {
        // Start entity spawning
        EntitySpawner entitySpawnerScript = entitySpawner.GetComponent<EntitySpawner>();
        entitySpawnerScript.SpawnEntities();

        // Fade out falling audio
        StartCoroutine(AudioFader.StartFade(fallingAudio, 4.0f, 0.0f));

        // Blend in entity background audio
        entityBackgroundAudio.volume = 0.0f;
        entityBackgroundAudio.Play();
        StartCoroutine(AudioFader.StartFade(entityBackgroundAudio, 8.0f, 1.0f));
    }
}
