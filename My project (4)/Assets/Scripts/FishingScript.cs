using System;
using System.Collections;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public bool isCasted;
    public GameObject baitPrefab;
    public GameObject rod;
    private float throwPower;
    public float minThrowDistance = 4f;
    public float maxThrowDistance = 20f;
    public float ThrowMultiplier = 30f;
    public Vector3 BaitSpawnPosition = new Vector3(4,6,-10);
    private Vector3 startRodPosition = new Vector3(1.13f, 1.01f, -4.3f);
    private Quaternion startRodRotation = Quaternion.Euler(-19.98f, -0.56f, 0);
    private Vector3 endRodPosition = new Vector3(1.13f, 2.26f, -5.77f);
    private Quaternion endRodRotation = Quaternion.Euler(-76.102f, -2.192f, 1.936f);
    public float lerpDuration = 1.0f;
    private bool isIncreasingPower = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCasted)
        {
            throwPower = minThrowDistance;
            isIncreasingPower = true;
        }

        if (Input.GetKey(KeyCode.Space) && !isCasted)
        {
            Debug.Log(throwPower);
            if (isIncreasingPower)
            {
                throwPower += Time.deltaTime*ThrowMultiplier;
                if (throwPower >= maxThrowDistance)
                {
                    throwPower = maxThrowDistance;
                    isIncreasingPower = false;
                }
            }
            else
            {
                throwPower -= Time.deltaTime*ThrowMultiplier;
                if (throwPower <= minThrowDistance)
                {
                    throwPower = minThrowDistance;
                    isIncreasingPower = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && !isCasted)
        {
            
            CastBait();
        }
    }

    private void CastBait()
    {
        
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * throwPower;
        Debug.Log(targetPosition);
        // targetPosition.z = Math.Clamp(targetPosition.z, -1.5f, 11);
        GameObject bait = Instantiate(baitPrefab, BaitSpawnPosition, Quaternion.identity);
        StartCoroutine(MoveBaitToTarget(bait.transform, targetPosition));

        isCasted = true;
    }

    private IEnumerator MoveBaitToTarget(Transform baitTransform, Vector3 targetPosition)
    {
        float journey = 0;
        Vector3 startPosition = baitTransform.position;

        while (journey <= 1)
        {
            journey += Time.deltaTime;
            targetPosition.y = 0.13f;
            baitTransform.position = Vector3.Lerp(startPosition, targetPosition, journey);

            // Stop movement if bait reaches y = 0.13
            if (baitTransform.position.y <= 0.13f)
            {
                baitTransform.position = new Vector3(baitTransform.position.x, 0.13f, baitTransform.position.z);
                break;
            }

            yield return null;
        }

        isCasted = false;
    }
}
