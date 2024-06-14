using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public AudioClip[] audioClips;

    public static GameManager instance = null;

    public static Action onEndGame;

    public AudioSource entitiesBackgroundSound;
    public AudioSource endSound;


    private int connections = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public int AddConnection()
    {   
        connections++;
        // if(connections == 1)
        if(connections > audioClips.Length)
        {
            // start ending
            StartCoroutine(AudioFader.StartFade(entitiesBackgroundSound, 4.0f, 0.0f));
            onEndGame.Invoke();
            endSound.Play();

            // after 15 seconds invoke a function that reloads the scene
            Invoke("ReloadScene", 35.0f);
        }
        return connections;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public AudioClip GetRandomAudioClip()
    {
        return audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
    }
    void Start()
    {
        // audioClips.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
