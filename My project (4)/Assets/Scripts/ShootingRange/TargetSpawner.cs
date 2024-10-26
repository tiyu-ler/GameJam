using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Spawning intervals and position ranges
    public float secondsBetweenSpawning = 0.1f;
    public float xMinRange = -25.0f;
    public float xMaxRange = 25.0f;
    public float yMinRange = 8.0f;
    public float yMaxRange = 25.0f;
    public float zMinRange = -25.0f;
    public float zMaxRange = 25.0f;
    public GameObject[] spawnObjects;

    public int maxTargets = 25; // Limit for the number of targets

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + secondsBetweenSpawning;
    }

    void Update()
    {

        if (Time.time >= nextSpawnTime && transform.childCount < maxTargets)
        {
            MakeThingToSpawn();
            nextSpawnTime = Time.time + secondsBetweenSpawning;
        }
    }

    void MakeThingToSpawn()
    {
        Vector3 spawnPosition;
        spawnPosition.x = Random.Range(xMinRange, xMaxRange);
        spawnPosition.y = Random.Range(yMinRange, yMaxRange);
        spawnPosition.z = Random.Range(zMinRange, zMaxRange);

        int objectToSpawn = Random.Range(0, spawnObjects.Length);
        GameObject spawnedObject = Instantiate(spawnObjects[objectToSpawn], spawnPosition, transform.rotation);
        spawnedObject.transform.parent = gameObject.transform;
    }
}
