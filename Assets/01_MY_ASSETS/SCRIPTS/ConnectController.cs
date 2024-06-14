using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Collections;
using System;
using Unity.XR.CoreUtils;

public class ConnectController : MonoBehaviour
{
    public float inputRangeMin = 0.0f;
    public float inputRangeMax = 5.0f;
    public AnimationCurve valueMapping;
    public float outputRangeMin = 1.0f;
    public float outputRangeMax = 10.0f;

    private GameObject _connectedEntity;
    private Collider _myCollider;

    private bool someEntityIsConnected = false;
     
    TransformWriter transformWriter;

    // Start is called before the first frame update

    void OnEnable(){
        GazeMasterScript.onGazeTimeUpdate += UpdateTargetValue;
        EntityController.onConnectionStateUpdate += SaveConnectionState;
    }

    void OnDisable(){
        GazeMasterScript.onGazeTimeUpdate -= UpdateTargetValue;
        EntityController.onConnectionStateUpdate -= SaveConnectionState;
    }
    
    void Start()
    {
        transformWriter = GetComponent<TransformWriter>();
        _myCollider = GetComponent<Collider>();
    }

    void SaveConnectionState(bool connection)
    {
        someEntityIsConnected = connection;
    }

    void Update()
    {   

    }

    void OnTriggerEnter(Collider other)
    {
        _connectedEntity = other.gameObject;
    }

    private float lastGazeTime = 0.0f;

    void UpdateTargetValue(float gazeTime)
    {
        float normalizedValue = Mathf.InverseLerp(inputRangeMin, inputRangeMax, gazeTime);
        float mappedValue = valueMapping.Evaluate(normalizedValue);
        float outputValue = Mathf.Lerp(outputRangeMin, outputRangeMax, mappedValue);


        if(!someEntityIsConnected)
        {   
            // usual
            transform.localPosition = new Vector3(0,-0.6f,outputValue);

            // only make collider active again if gazeTime is increasing
            // (prevents accidental connection when giving up focus)
            if (gazeTime > lastGazeTime) _myCollider.enabled = true;

        }else{
            // entity connected
            // set x and z position to connected entity
            transform.position = new Vector3(_connectedEntity.transform.position.x, _connectedEntity.transform.position.y + 1.0f, _connectedEntity.transform.position.z);
            _myCollider.enabled = false;
        }

        lastGazeTime = gazeTime;
        
    }
}
