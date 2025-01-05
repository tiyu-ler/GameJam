using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> prefabs;
    public bool IsSpawning;
    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 1.8f;
    public float multipleSpawnChance = 0.3f;
    void Start()
    {
        IsSpawning = true;
        StartCoroutine(Spawners());
    }

    private IEnumerator Spawners()
    {
        while (IsSpawning)
        {
            float randomInterval = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomInterval);

            int spawnCount = 1; // Default is one prefab
            if (Random.value < multipleSpawnChance) spawnCount = Random.Range(2, 4);

            // Spawn the prefabs
            List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);
            for (int i = 0; i < spawnCount; i++)
            {
                if (availableSpawnPoints.Count > 0)
                {
                    // Randomly select a spawn point and remove it from the list to avoid reuse
                    int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
                    Transform spawnPoint = availableSpawnPoints[spawnIndex];
                    availableSpawnPoints.RemoveAt(spawnIndex);

                    // Select a random prefab
                    GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];

                    // Spawn the prefab
                    Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                }
            }
            // Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];

            // Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
