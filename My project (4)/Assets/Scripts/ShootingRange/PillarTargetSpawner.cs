using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarTargetSpawner : MonoBehaviour
{
    public float spawnDelay = 2f;
    public float riseSpeed = 2f;
    public GameObject[] TargetsToSpawn;
    public int MaxAmountPerType = 5;
    public float[] SpawnChances;
    private Vector3 SpawnPoint;
    public float MaxHeight = 10f;
    float timer = 0f;
    private GameObject ChildTarget;

    void Start()
    {
        if (TargetsToSpawn.Length != SpawnChances.Length)
        {
            Debug.LogError("TargetsToSpawn and SpawnChances have different lengths");
        }
        SpawnPoint=transform.position;
        SpawnTarget();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay)
        {
            timer = 0f;
            SpawnTarget();
        }
    }

    void SpawnTarget()
    {
        for (int i = 0; i < TargetsToSpawn.Length; i++)
        {
            GameObject targetPrefab = TargetsToSpawn[i];
            int currentCount = CountTargetsOfType(targetPrefab.name);

            if (currentCount >= MaxAmountPerType||ChildTarget!=null)
                continue;

            float spawnChance = Random.Range(0f, 1f);
            if (spawnChance <= SpawnChances[i])
            {
                GameObject newTarget = Instantiate(targetPrefab, SpawnPoint, Quaternion.identity);
                ChildTarget=newTarget;
                StartCoroutine(MoveTargetUpwards(newTarget));
                
                break;
            }
        }
    }

    int CountTargetsOfType(string targetName)
    {
        int count = 0;
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject target in allTargets)
        {
            if (target.name.Contains(targetName))
            {
                count++;
            }
        }
        return count;
    }

    IEnumerator MoveTargetUpwards(GameObject target)
    {
        while (target.transform.position.y < MaxHeight)
        {
            target.transform.position += Vector3.up * riseSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
