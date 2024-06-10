using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{   

    public GameObject ghostPrefab;


    // spawn a threedimensional grid of thousands of ghosts
    void SpawnGhosts()
    {
        for (int x = -20; x < 20; x +=3)
        {
            for (int y = -20; y < 20; y+=3)
            {
                for (int z = -20; z < 20; z+=3)
                {   
                    

                    
                    // instantiate each ghost a random x, y, z offset in a specified range
                    GameObject ghost = Instantiate(ghostPrefab, new Vector3(x + Random.Range(-1, 1), y + Random.Range(-1, 1), z + Random.Range(-1, 1)), Quaternion.identity);

                    // create random rotation (quaternion) around y axis
                    ghost.transform.Rotate(0, Random.Range(0, 360), 0);

                    // GameObject ghost = Instantiate(ghostPrefab, new Vector3(x, y, z), Quaternion.identity);
                    ghost.transform.parent = transform;
                }
            }
        }
    }
    // call spawn ghosts when the game starts
    void Awake()
    {
        SpawnGhosts();
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
