using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowpassDistance : MonoBehaviour
{
    public GameObject RandomNPC; // Référence à l'objet A (celui qui a le son)
    public GameObject Pole; // Référence à l'objet B (celui qui se déplace)
    public AudioLowPassFilter lowPassFilter; // Référence au filtre low pass

    public float maxDistance = 20.0f; // Distance maximale à laquelle le filtre est complètement activé
    public float minCutoffFrequency = 500.0f; // Fréquence de coupure minimale du filtre
    public float maxCutoffFrequency = 22000.0f; // Fréquence de coupure maximale du filtre

    void Start()
    {
        if (RandomNPC == null || Pole == null || lowPassFilter == null)
        {
            Debug.LogError("Toutes les références doivent être définies !");
            enabled = false; // Désactive le script si les références ne sont pas définies
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(RandomNPC.transform.position, Pole.transform.position);
        float t = Mathf.Clamp01(distance / maxDistance);
        float cutoffFrequency = Mathf.Lerp(maxCutoffFrequency, minCutoffFrequency, t);
        lowPassFilter.cutoffFrequency = cutoffFrequency;
    }
}