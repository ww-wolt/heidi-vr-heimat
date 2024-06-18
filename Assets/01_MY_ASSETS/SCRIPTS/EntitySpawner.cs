using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entityPrefab;

    public int entityCount = 200;

    public float spawnTime = 5.0f;

    public bool spawnOnStart = false;


    // spawn a threedimensional grid of thousands of entities
    public void SpawnEntities()
    {
       StartCoroutine(SpawningCoroutine());
    }

    private IEnumerator SpawningCoroutine()
    {
        for (int i = 0; i < entityCount; i++)
        {
            GameObject entity = Instantiate(entityPrefab, Vector3.zero, Quaternion.identity);
            entity.transform.parent = transform;
            yield return new WaitForSeconds(spawnTime / entityCount);
        }
    }
    // call spawn entitys when the game starts
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (spawnOnStart)
        {
            SpawnEntities();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
