using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip[] audioClips;

    public static GameManager instance = null;

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
        if(connections > audioClips.Length)
        {
            // start ending
            // reload scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        return connections;
    }

    public AudioClip GetRandomAudioClip()
    {
        return audioClips[Random.Range(0, audioClips.Length)];
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
