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
    public Vector3 BaitSpawnPosition = new Vector3(4, 6, -10);
    private Vector3 startRodPosition = new Vector3(1.13f, 1.01f, -4.3f);
    private Quaternion startRodRotation = Quaternion.Euler(-19.98f, -0.56f, 0);
    private Vector3 endRodPosition = new Vector3(1.13f, 2.26f, -5.77f);
    private Quaternion endRodRotation = Quaternion.Euler(-76.102f, -2.192f, 1.936f);
    public float lerpDuration = 1.0f;
    private bool isIncreasingPower = true;

    private float time = 0;  
    private bool Toss;  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCasted)
        {
            time = 0;
            Toss = false;
        }

        if (Input.GetKey(KeyCode.Space) && !isCasted)
        {
            time += Time.deltaTime;
            if (time > 0.15f && !Toss)
            {
                Toss = true;
                StartCoroutine(RaiseRod());
                isIncreasingPower = true;
                throwPower = minThrowDistance;
            }
            if (Toss)
            {
            if (isIncreasingPower)
            {
                throwPower += Time.deltaTime * ThrowMultiplier;
                if (throwPower >= maxThrowDistance)
                {
                    throwPower = maxThrowDistance;
                    isIncreasingPower = false;
                }
            }
            else
            {
                throwPower -= Time.deltaTime * ThrowMultiplier;
                if (throwPower <= minThrowDistance)
                {
                    throwPower = minThrowDistance;
                    isIncreasingPower = true;
                }
            }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && !isCasted && Toss)
        {
            StartCoroutine(ThrowRod());
        }
    }

    private IEnumerator RaiseRod()
    {
        yield return new WaitForEndOfFrame();
        float elapsedTime = 0f;
        while (elapsedTime < lerpDuration)
        {
            rod.transform.position = Vector3.Lerp(startRodPosition, endRodPosition, elapsedTime / lerpDuration);
            rod.transform.rotation = Quaternion.Lerp(startRodRotation, endRodRotation, elapsedTime / lerpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rod.transform.position = endRodPosition;
        rod.transform.rotation = endRodRotation;
    }

    private IEnumerator ThrowRod()
    {
        yield return new WaitForEndOfFrame();
        float elapsedTime = 0f;
        while (elapsedTime < lerpDuration)
        {
            rod.transform.position = Vector3.Lerp(endRodPosition, startRodPosition, elapsedTime / lerpDuration);
            rod.transform.rotation = Quaternion.Lerp(endRodRotation, startRodRotation, elapsedTime / lerpDuration);
            elapsedTime += Time.deltaTime * 2;
            yield return null;
        }
        rod.transform.position = startRodPosition;
        rod.transform.rotation = startRodRotation;

        StartCoroutine(CastBait());
    }

    private IEnumerator CastBait()
    {   
        yield return new WaitForEndOfFrame();
        Debug.Log(throwPower);
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * throwPower;
        Debug.Log(targetPosition);
        GameObject bait = Instantiate(baitPrefab, BaitSpawnPosition, Quaternion.identity);
        StartCoroutine(MoveBaitToTarget(bait.transform, targetPosition));
        isCasted = true;
        yield return null;
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
            if (baitTransform.position.y <= 0.13f)
            {
                baitTransform.position = new Vector3(baitTransform.position.x, 0.13f, baitTransform.position.z);
                break;
            }
            yield return null;
        }
        isCasted = false; // Reset for the next cast
    }
}
