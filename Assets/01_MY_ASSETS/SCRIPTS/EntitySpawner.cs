using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
     public GameObject entityPrefab;

     public int entityCount = 200;


    // spawn a threedimensional grid of thousands of entities
    void SpawnEntities()
    {

        for (int i = 0; i < entityCount; i++)
        {
            // float distance = Random.Range(spawnMinDistance, spawnMaxDistance);
            // float angle = Random.Range(0, 360);
            // Vector3 randomPosition = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * distance;
            
            // instantiate each entity a random x, y, z offset in a specified range
            GameObject entity = Instantiate(entityPrefab, Vector3.zero, Quaternion.identity);

            // create random rotation around y axis
            // entity.transform.Rotate(0, Random.Range(0, 360), 0);

            entity.transform.parent = transform;
        }
    }
    // call spawn entitys when the game starts
    void Awake()
    {
        SpawnEntities();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
