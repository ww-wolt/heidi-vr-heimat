using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class EntityController : MonoBehaviour
{

    public static Action<bool> onConnectionStateUpdate;


    public float walkingSpeed = 1.0f;

    public float spawnMinDistance = 20.0f;
    public float spawnMaxDistance = 40.0f;

    public float evasionAngle = 4.0f;


    private GameObject mainCamera;

    // private variable that stores whether entity got connected by player
    private bool myselfIsConnected = false;
    private bool someEntityIsConnected = false;

    private float gazeTime = 0.0f;




    private Animator walkingAnimator;
    private SkinnedMeshRenderer myRenderer;
    private Color baseEmissionColor;

    private GameObject endPoint;

    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");

        repositionMyself();
    }
    void Start()
    {
        // get animator component in child
        walkingAnimator = GetComponentInChildren<Animator>();

        GazeMasterScript.onGazeTimeUpdate += SaveGazeTime;
        EntityController.onConnectionStateUpdate += SaveConnectionState;

        myRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        baseEmissionColor = myRenderer.material.GetColor("_EmissionColor");


        // store time.time
        float connectionTime = Time.time;
    }

    void SaveGazeTime(float gazeTime)
    {
        // Reset connection state if gaze is going away
        if (this.gazeTime > gazeTime)
        {
            onConnectionStateUpdate?.Invoke(false);
        }
        this.gazeTime = gazeTime;
    }

    void SaveConnectionState(bool connection)
    {
        // Debug.Log("Entity: Received new Connection State: " + connection);
        // if (!myselfIsConnected){
        someEntityIsConnected = connection;
        // }
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


            // lerp 0.1 into direction of connection Vector
            transform.forward = Vector3.Lerp(transform.forward, connectionVector, 0.15f * Time.deltaTime);
            walkingSpeed = Mathf.Max(walkingSpeed - Time.deltaTime * 0.18f, 0.0f);
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
                myRenderer.material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f));
                // myRenderer.material.SetColor("_EmissionColor", lerpedColor);
            }
            else
            {
                myRenderer.material.SetColor("_EmissionColor", baseEmissionColor);
            }

        }

        if(myselfIsConnected && someEntityIsConnected){
            endPoint.GetComponent<Collider>().enabled = false;
            endPoint.transform.position = transform.position;
        }else{
            if(endPoint) endPoint.GetComponent<Collider>().enabled = true;
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
    }

    // trigger enter
    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Entity: Collision with " + other.name);

        if (other.tag == "ConnectionLine")
        {
            myselfIsConnected = true;

            // myRenderer.material.SetColor("_BaseColor", new Color(1.0f, 0.447f, 0.4196f));  
            myRenderer.material.SetColor("_EmissionColor", new Color(1.0f, 0.447f, 0.4196f) * 2.0f);


            onConnectionStateUpdate?.Invoke(true);

            endPoint = other.gameObject;

            // GetComponentInChildren<Renderer>().material.color = new Color(1.0f, 0.447f, 0.4196f);




        }
    }
}
