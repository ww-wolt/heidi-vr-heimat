using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWalker : MonoBehaviour
{

    public static Action<bool> onEntityConnected;
    public float walkingSpeed = 1.0f;

    public float spawnMinDistance = 20.0f;
    public float spawnMaxDistance = 40.0f;

    public float evasionAngle = 4.0f;

    // private variable that stores whether entity got connected by player
    private bool isConnected = false;

    private Animator walkingAnimator;

    void Awake()
    {
        repositionMyself();
    }
    void Start()
    {
        // get animator component in child
        walkingAnimator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        walkingAnimator.speed = walkingSpeed;
        transform.position += transform.forward * Time.deltaTime * walkingSpeed;

        if (Vector3.Distance(Vector3.zero, transform.position) > spawnMaxDistance)
        {
            repositionMyself();
        }
    }

    private void repositionMyself(){
        float distance = UnityEngine.Random.Range(spawnMinDistance, spawnMaxDistance);
        float angle = UnityEngine.Random.Range(0, 360);
        Vector3 randomPosition = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * distance;

        transform.position = randomPosition;


        // ConnectionVector to the center
        Vector3 connectionVector = Vector3.zero - transform.position;

        transform.forward = connectionVector;

        transform.Rotate(0, UnityEngine.Random.Range(evasionAngle, 360-evasionAngle), 0);
    }

    // trigger enter
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entity: Collision with " + other.name);

        if (other.tag == "ConnectionLine")
        {
            isConnected = true;

            // GetComponentInChildren<Renderer>().material.color = new Color(1.0f, 0.447f, 0.4196f);

            // get skinned mesh renderer and set emmisive and base color to red
            SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            Debug.Log("skinnedMeshRenderer: " + skinnedMeshRenderer);
            skinnedMeshRenderer.material.SetColor("_BaseColor", new Color(1.0f, 0.447f, 0.4196f));  
            skinnedMeshRenderer.material.SetColor("_EmissionColor", new Color(1.0f, 0.447f, 0.4196f));
        }
    }
}
