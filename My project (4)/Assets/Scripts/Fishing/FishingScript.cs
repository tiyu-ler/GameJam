using System.Collections;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public FishingCrosshairScript crosshairScript;
    public GameObject Crosshair;
    public bool isCasted;
    public GameObject baitPrefab;
    public GameObject rod;
    // private float throwPower;
    // public float minThrowDistance = 4f;
    // public float maxThrowDistance = 20f;
    // public float ThrowMultiplier = 30f;
    public Vector3 BaitSpawnPosition = new Vector3(-5.14f, 2.51f, 0.92f);
    private Vector3 startRodPosition = new Vector3(-7.04f, 0.85f, 0.84f);
    private Quaternion startRodRotation = Quaternion.Euler(-19.98f, -87.163f, 0);
    private Vector3 endRodPosition = new Vector3(-5.83f, 1.09f, 0.84f);
    private Quaternion endRodRotation = Quaternion.Euler(-82.9f, -87.163f, 0f);
    public float lerpDuration = 1.0f;
    // private bool isIncreasingPower = true;

    private float time = 0;  
    private bool Toss;  
    void Start()
    {
        crosshairScript = Crosshair.GetComponent <FishingCrosshairScript>();
    }
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
                // isIncreasingPower = true;
                // throwPower = minThrowDistance;
                crosshairScript.LetMove();
            }
            // if (Toss)
            // {
            // if (isIncreasingPower)
            // {
            //     throwPower += Time.deltaTime * ThrowMultiplier;
            //     if (throwPower >= maxThrowDistance)
            //     {
            //         throwPower = maxThrowDistance;
            //         isIncreasingPower = false;
            //     }
            // }
            // else
            // {
            //     throwPower -= Time.deltaTime * ThrowMultiplier;
            //     if (throwPower <= minThrowDistance)
            //     {
            //         throwPower = minThrowDistance;
            //         isIncreasingPower = true;
            //     }
            // }
            // }
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
        // Debug.Log(throwPower);
        Vector3 targetPosition = crosshairScript.Stop();
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
            baitTransform.position = Vector3.Lerp(startPosition, targetPosition, journey);
            yield return null;
        }
        isCasted = false; // Reset for the next cast
    }
}
