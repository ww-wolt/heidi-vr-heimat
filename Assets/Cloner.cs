using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{
    public GameObject Prefab;

    public int xAmount = 3;
    public int yAmount = 3;
    public int zAmount = 3;
    public float spacing = 3.0f;

    // spawn a three-dimensional grid of ghosts
    void SpawnPrefabs()
    {
        for (int x = -xAmount; x < xAmount; x += (int)spacing)
        {
            for (int y = -yAmount; y < yAmount; y += (int)spacing)
            {
                for (int z = -zAmount; z < zAmount; z += (int)spacing)
                {
                    // instantiate each ghost with a random x, y, z offset in a specified range
                    GameObject ghost = Instantiate(Prefab, new Vector3(x + Random.Range(-1f, 1f), y + Random.Range(-1f, 1f), z + Random.Range(-1f, 1f)), Quaternion.identity);

                    // create random rotation (quaternion) around y-axis
                    ghost.transform.Rotate(0, Random.Range(0, 360), 0);

                    // assign the ghost as a child of this GameObject
                    ghost.transform.parent = transform;
                }
            }
        }
    }

    // call spawn ghosts when the game starts
    void Awake()
    {
        SpawnPrefabs();
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

