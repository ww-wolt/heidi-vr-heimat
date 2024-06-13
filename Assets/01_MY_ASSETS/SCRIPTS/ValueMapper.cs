using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Collections;

public class ValueMapper : MonoBehaviour
{
    public enum Input {
        GazeTime,
        ConnectionTime,
    }

    public Input input = Input.GazeTime;
    public float inputRangeMin = 0.0f;
    public float inputRangeMax = 5.0f;
    public AnimationCurve valueMapping;
    public float outputRangeMin = 1.0f;
    public float outputRangeMax = 10.0f;

    public UnityEvent<float> targetValue;

    [ReadOnly] public float outputReadOnly;
     
    // Start is called before the first frame update
    void Start()
    {
        if(input == Input.GazeTime){
            GazeMasterScript.onGazeTimeUpdate += UpdateTargetValue;
        } else if(input == Input.ConnectionTime){
            // EntityController.onConnectionTimeUpdate += UpdateTargetValue;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        float newValue = Mathf.PingPong(Time.time, 10f);
        
    }

    void UpdateTargetValue(float gazeTime)
    {
        float normalizedValue = Mathf.InverseLerp(inputRangeMin, inputRangeMax, gazeTime);
        float mappedValue = valueMapping.Evaluate(normalizedValue);
        float outputValue = Mathf.Lerp(outputRangeMin, outputRangeMax, mappedValue);
        targetValue.Invoke(outputValue);
        outputReadOnly = outputValue;
    }
}
