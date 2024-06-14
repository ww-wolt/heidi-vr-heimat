using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class EntityController : MonoBehaviour
{

    public static Action<bool> onConnectionStateUpdate;


    public float walkingSpeed = 1.0f;

    public float spawnMinDistance = 20.0f;
    public float spawnMaxDistance = 40.0f;

    public float evasionAngle = 4.0f;

    public AudioMixerGroup voicesClearGroup;
    public AudioMixerGroup voicesDistortedGroup;
    


    private GameObject mainCamera;

    // private variable that stores whether entity got connected by player
    private bool myselfIsConnected = false;
    private bool someEntityIsConnected = false;

    private float gazeTime = 0.0f;


    private Animator walkingAnimator;
    private SkinnedMeshRenderer myRenderer;
    private Color baseEmissionColor;

    private AudioSource audioSource;
    private AudioClip voiceExcerpt;

    private int myConnectionNr = -1;

    private bool endGame = false;

    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        audioSource = GetComponent<AudioSource>();
        repositionMyself();
    }
    void Start()
    {

        // get animator component in child
        walkingAnimator = GetComponentInChildren<Animator>();

        GazeMasterScript.onGazeTimeUpdate += SaveGazeTime;
        EntityController.onConnectionStateUpdate += SaveConnectionState;
        GameManager.onEndGame += OnEndGame;

        myRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        baseEmissionColor = myRenderer.material.GetColor("_EmissionColor");
    }

    void SaveGazeTime(float gazeTime)
    {
        // Reset global connection state if gaze is going away
        if (myselfIsConnected && someEntityIsConnected && this.gazeTime > gazeTime )
        {
            onConnectionStateUpdate?.Invoke(false);
            Debug.Log("Reset Connection State + Fade out audio");
            StartCoroutine(AudioFader.StartFade(audioSource, 3.0f, 0.0f));
        }
        this.gazeTime = gazeTime;
    }

    void SaveConnectionState(bool connection)
    {
        someEntityIsConnected = connection;
    }

    void OnEndGame()
    {
        endGame = true;
    }

    void Update()
    {
        walkingAnimator.speed = walkingSpeed;
        transform.position += transform.forward * Time.deltaTime * walkingSpeed;

        if (Vector3.Distance(mainCamera.transform.position, transform.position) > spawnMaxDistance)
        {
            repositionMyself();
        }


        if (myselfIsConnected)
        {

            Vector3 connectionVector = mainCamera.transform.position - transform.position;
            connectionVector.y = 0.0f;

            if(endGame){
                // lerp 0.1 into direction of connection Vector
                transform.forward = Vector3.Lerp(transform.forward, connectionVector, 0.15f * Time.deltaTime);
                walkingSpeed = Mathf.Min(walkingSpeed + Time.deltaTime * 0.08f, 0.36f);
                myRenderer.material.SetColor("_EmissionColor", new Color(1.0f, 0.447f, 0.4196f) * 8.0f);
            }else{
                 // lerp 0.1 into direction of connection Vector
                transform.forward = Vector3.Lerp(transform.forward, connectionVector, 0.15f * Time.deltaTime);
                walkingSpeed = Mathf.Max(walkingSpeed - Time.deltaTime * 0.18f, 0.0f);
                myRenderer.material.SetColor("_EmissionColor", new Color(1.0f, 0.447f, 0.4196f) * 2.0f);
            }
        }
        else
        {
            if (someEntityIsConnected)
            {
                // Lerp transparency to 0.5
                // GetComponentInChildren<Renderer>().material.color = new Color(1.0f, 0.447f, 0.4196f, 0.5f);

                Color lerpedColor = Color.Lerp(baseEmissionColor, new Color(0.02f, 0.02f, 0.02f), Time.deltaTime * 10.0f);
                // Debug.Log("Lerped Color: " + lerpedColor);

                // myRenderer.material.SetColor("_BaseColor", new Color(1.0f, 1.0f, 1.0f, 0.1f)); 
                myRenderer.material.SetColor("_EmissionColor", new Color(0.05f, 0.05f, 0.05f));
                // myRenderer.material.SetColor("_EmissionColor", lerpedColor);
            }
            else
            {
                myRenderer.material.SetColor("_EmissionColor", baseEmissionColor);
            }
            if(endGame){
                StartCoroutine(AudioFader.StartFade(audioSource, UnityEngine.Random.Range(1,4), 0.0f));
            }

        }
        
    }

    private void repositionMyself()
    {
        float distance = UnityEngine.Random.Range(spawnMinDistance, spawnMaxDistance);
        float angle = UnityEngine.Random.Range(0, 360);
        Vector3 randomPosition = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * distance;

        transform.position = randomPosition;


        // ConnectionVector to the center
        Vector3 connectionVector = mainCamera.transform.position - transform.position;
        connectionVector.y = 0.0f;
        transform.forward = connectionVector;


        transform.Rotate(0, UnityEngine.Random.Range(evasionAngle, 360 - evasionAngle), 0);

        voiceExcerpt = GameManager.instance.GetRandomAudioClip();
        audioSource.Stop();
        audioSource.outputAudioMixerGroup = voicesDistortedGroup;
        audioSource.volume = 0.3f;
        audioSource.spatialBlend = 1.0f;
        audioSource.clip = voiceExcerpt;
        audioSource.Play();
    }

    // trigger enter
    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Trigger Enter: " + other.name);
        // Myself got connected
        if (other.tag == "ConnectionLine")
        {
            myselfIsConnected = true;

            // myRenderer.material.SetColor("_BaseColor", new Color(1.0f, 0.447f, 0.4196f));  

            // Debug.Log("myRenderer material: " + myRenderer.material);


            onConnectionStateUpdate?.Invoke(true);
            // hacky solution to prevent bugs when lasso is going back and catching something
            GazeMasterScript.instance.SetGazeTimeToMax();

            // only get a new index and audio clip if its the first time
            if(myConnectionNr < 0) myConnectionNr = GameManager.instance.AddConnection();

            if(!endGame){
                // Start audio clip
                Debug.Log("Start Audio Clip: " + myConnectionNr);
                audioSource.Stop();
                audioSource.outputAudioMixerGroup = voicesClearGroup;
                audioSource.volume = 0.0f;
                audioSource.spatialBlend = 0.0f;
                audioSource.clip = GameManager.instance.audioClips[myConnectionNr-1];
                audioSource.Play();
                StartCoroutine(AudioFader.StartFade(audioSource, 0.7f, 1.0f));
            }
            // GetComponentInChildren<Renderer>().material.color = new Color(1.0f, 0.447f, 0.4196f);
        }
    }
}
