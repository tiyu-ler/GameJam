using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarTargetSpawner : MonoBehaviour
{
    public enum Direction { X, Y, Z }

    public float minspawnDelay = 2f;
    public float maxspawnDelay = 2f;
    public float riseSpeed = 2f;
    public GameObject[] TargetsToSpawn;
    public float[] SpawnChances;
    private Vector3 SpawnPoint;
    public float MaxHeight = 10f;
    public Direction moveDirection = Direction.Y;
    public bool invert = false;
    public float objectScale = 1f;
    float timer = 0f;
    private GameObject ChildTarget;
    private Coroutine moveCoroutine;

    void Start()
    {
        if (TargetsToSpawn.Length != SpawnChances.Length)
        {
            Debug.LogError("TargetsToSpawn and SpawnChances have different lengths");
        }
        SpawnPoint = transform.position;
        SpawnTarget();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= Random.Range(minspawnDelay, maxspawnDelay))
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

            if (ChildTarget != null)
                continue;

            if (Random.Range(0f, 1f) <= SpawnChances[i])
            {
                GameObject newTarget = Instantiate(targetPrefab, SpawnPoint, Quaternion.identity);
                ChildTarget = newTarget;

                // Apply uniform scale relative to the object's original scale
                float uniformScale = objectScale;
                newTarget.transform.localScale = newTarget.transform.localScale * uniformScale;

                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(MoveTarget(newTarget));

                break;
            }
        }
    }

    IEnumerator MoveTarget(GameObject target)
    {
        Vector3 moveDirectionVector = GetMoveDirectionVector();
        Vector3 startPosition = target.transform.position;
        Vector3 targetPosition = startPosition + (moveDirectionVector * MaxHeight);

        while (target != null && !(Vector3.Distance(target.transform.position, targetPosition) < 0.01f))
        {
            target.transform.position += moveDirectionVector * riseSpeed * Time.deltaTime;
            yield return null;
        }
    }

    Vector3 GetMoveDirectionVector()
    {
        Vector3 directionVector = Vector3.zero;

        switch (moveDirection)
        {
            case Direction.X:
                directionVector = Vector3.right;
                break;
            case Direction.Y:
                directionVector = Vector3.up;
                break;
            case Direction.Z:
                directionVector = Vector3.forward;
                break;
        }

        if (invert)
        {
            directionVector = -directionVector;
        }

        return directionVector;
    }
}
