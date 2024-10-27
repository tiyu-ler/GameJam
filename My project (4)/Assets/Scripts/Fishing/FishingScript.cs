using System;
using System.Collections;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public FishingCrosshairScript crosshairScript;
    public GameObject Crosshair;
    private GameObject CrosshairGameObject;
    public GameObject Ciel;
    public bool isCasted;
    public GameObject baitPrefab;
    private GameObject baitGameObject;
    public GameObject rod;
    public Vector3 BaitSpawnPosition = new Vector3(-5.14f, 2.51f, 0.92f);
    private Quaternion BaitSpawnRotation = Quaternion.Euler(-19.98f, -87.163f, 0);
    private Vector3 startRodPosition = new Vector3(-7.04f, 0.85f, 0.84f);
    private Quaternion startRodRotation = Quaternion.Euler(-19.98f, -87.163f, 0);
    private Vector3 endRodPosition = new Vector3(-5.83f, 1.09f, 0.84f);
    private Quaternion endRodRotation = Quaternion.Euler(-82.9f, -87.163f, 0f);
    public float lerpDuration = 1.0f;
    
    private float time = 0;  
    private bool Toss;  
    // public void NewToss()
    // {
    //     Instantiate(Ciel, new Vector3(UnityEngine.Random.Range(-12.0f, -22.0f),0,0), Quaternion.identity);
    // }
    // void Start()
    // {
    //     NewToss();
    // }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCasted)
        {
            time = 0;
            Toss = false;
            if (CrosshairGameObject != null)
            {
                Destroy(CrosshairGameObject);
            }
            if (baitGameObject != null)
            {
                Destroy(baitGameObject);
            }
            CrosshairGameObject = Instantiate(Crosshair, new Vector3(-20, -0.56f, -0.13f), Quaternion.Euler(90, 0, 0));
            crosshairScript = CrosshairGameObject.GetComponent <FishingCrosshairScript>();
        }

        if (Input.GetKey(KeyCode.Space) && !isCasted)
        {
            time += Time.deltaTime;
            if (time > 0.15f && !Toss)
            {
                Toss = true;
                crosshairScript.LetMove();
                StartCoroutine(RaiseRod());
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
        Vector3 targetPosition = crosshairScript.Stop();
        targetPosition = new Vector3 (targetPosition.x, -0.9f, targetPosition.z);
        Destroy(CrosshairGameObject);
        baitGameObject = Instantiate(baitPrefab, BaitSpawnPosition, BaitSpawnRotation);
        StartCoroutine(MoveBaitToTarget(baitGameObject.transform, targetPosition));
        isCasted = true;
        yield return null;
    }

    private IEnumerator MoveBaitToTarget(Transform baitTransform, Vector3 targetPosition)
    {
        float journey = 0;
        Vector3 startPosition = baitTransform.position;
        while (journey <= 1)
        {
            journey += Time.deltaTime * 2;
            baitTransform.position = Vector3.Lerp(startPosition, targetPosition, journey);
            baitTransform.rotation = Quaternion.Lerp(BaitSpawnRotation, Quaternion.Euler(0,0,0), journey);
            yield return null;
        }
        isCasted = false;
    }
}