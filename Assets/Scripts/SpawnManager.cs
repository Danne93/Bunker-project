using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float startDelay = 0.5f;
    public float spawnRate = 1.2f;
    private float spawnZPos = 42f;
    private float spawnXBounds = 50f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnUnit", startDelay, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 RandomSpawnPos()
    {
        Vector3 v = new Vector3(Random.Range(-spawnXBounds, spawnXBounds), 0.5f, spawnZPos);
        return v;
    }
    public void SpawnUnit()
    {
        Instantiate(enemyPrefabs[0], RandomSpawnPos(), enemyPrefabs[0].transform.rotation);
    }
}
