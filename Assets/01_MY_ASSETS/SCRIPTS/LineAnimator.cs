using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class LineAnimator : MonoBehaviour
{
    // public int numPoints = 40;

    public float pointsDistance = 0.1f;
    public int minPoints = 20;
    public float perlinScale = 0.75f;
    public float amplitude = 1.0f;
    public float movementSpeed = 0.5f;

    private GameObject start;
    private GameObject end;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        

        start = transform.Find("End").gameObject;
        end = transform.Find("Start").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = start.transform.position;
        Vector3 endPos = end.transform.position;

        Vector3 connectionVec = endPos - startPos;

        // Length of connection vector
        // float connectionLength = connectionVec.magnitude;

        int numPoints = Mathf.Max((int) (connectionVec.magnitude / pointsDistance), minPoints);
        lineRenderer.positionCount = numPoints;

        // Debug.Log("numPoint " + numPoints);

        var points = new Vector3[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            float t = i / ((float)numPoints-1);
            Vector3 pointPos = startPos + connectionVec * t;

            float distance = (pointPos - startPos).magnitude;

            float xOffset = Mathf.PerlinNoise1D(distance * perlinScale + Time.time * movementSpeed) - 0.5f;
            float yOffset = Mathf.PerlinNoise1D(distance * perlinScale + Time.time * movementSpeed + 1000) - 0.5f;
            float zOffset = Mathf.PerlinNoise1D(distance * perlinScale + Time.time * movementSpeed + 2000) - 0.5f;

            // Make amplitude smaller towards start and end of the line
            float endMultiplier = Mathf.Sin(t * Mathf.PI);

            points[i] = pointPos + new Vector3(xOffset * amplitude * endMultiplier, yOffset * amplitude * endMultiplier, zOffset * amplitude * endMultiplier);
        }
        lineRenderer.SetPositions(points);
    }
}
