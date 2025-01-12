using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fishing : MonoBehaviour
{
    public FishingCrosshairScript crosshairScript;
    public CielScript cielScript;
    public GameObject Crosshair;
    private GameObject CrosshairGameObject;
    public GameObject Ciel;
    public bool isCasted;
    public GameObject baitPrefab;
    private GameObject baitGameObject;
    public GameObject rod;
    public Vector3 BaitSpawnPosition = new Vector3(-5.14f, 2.51f, 1.9f);
    private Quaternion BaitSpawnRotation = Quaternion.Euler(-62.231f, -101.926f, -4.103f);
    private Vector3 startRodPosition = new Vector3(-7.04f, 0.85f, 0.84f);
    private Quaternion startRodRotation = Quaternion.Euler(-19.98f, -87.163f, 0);
    private Vector3 endRodPosition = new Vector3(-5.83f, 1.09f, 0.84f);
    private Quaternion endRodRotation = Quaternion.Euler(-82.9f, -87.163f, 0f);
    public float lerpDuration = 1.0f;
    public bool CanBeCatched = false;
    private bool fishingActive = true;
    private float time = 0;
    private bool Toss;
    //private SceneLoader sceneLoader;
    public stateHandler stateHandler;
    public void NewToss()
    {
        Instantiate(Ciel, new Vector3(UnityEngine.Random.Range(-12.0f, -20.0f), 0, 0), Quaternion.identity);
    }
    void Start()
    {
        fishingActive = true;
        CanBeCatched = false;
        cielScript = FindObjectOfType<CielScript>();
        NewToss();
       // sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void StopFishing()
    {
        Destroy(rod);
        Destroy();
        fishingActive = false;
        //     if (sceneLoader != null)
        // {
        SceneManager.LoadScene("Level_02");
        //  }
    }
    private void Update()
    {
        if (stateHandler.isPaused == false && stateHandler.isCompleted == false)
        {
            if (fishingActive)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !isCasted && !CanBeCatched)
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
                    crosshairScript = CrosshairGameObject.GetComponent<FishingCrosshairScript>();
                }

                if (Input.GetKey(KeyCode.Space) && !isCasted && !CanBeCatched)
                {
                    time += Time.deltaTime;
                    if (time > 0.15f && !Toss)
                    {
                        Toss = true;
                        crosshairScript.LetMove();
                        StartCoroutine(RaiseRod());
                    }
                }

                if (Input.GetKeyUp(KeyCode.Space) && !isCasted && Toss && !CanBeCatched)
                {
                    StartCoroutine(ThrowRod());
                }
            }
        }
    }

    public void Destroy()
    {
        if (CrosshairGameObject != null)
        {
            Destroy(CrosshairGameObject);
        }
        if (baitGameObject != null)
        {
            Destroy(baitGameObject);
        }
        if (crosshairScript != null)
        {
            Destroy(crosshairScript);
        }
        CanBeCatched = false;
    }
    public void ExternalRaise()
    {
        StartCoroutine(RaiseRod());
    }
    public void ExternalThrow()
    {
        StartCoroutine(ThrowRod());
    }

    private IEnumerator RaiseRod()
    {
        yield return new WaitForEndOfFrame();
        float elapsedTime = 0f;
        while (elapsedTime < lerpDuration)
        {
            if (rod != null)
            {
                rod.transform.position = Vector3.Lerp(startRodPosition, endRodPosition, elapsedTime / lerpDuration);
                rod.transform.rotation = Quaternion.Lerp(startRodRotation, endRodRotation, elapsedTime / lerpDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (rod != null)
        {
            rod.transform.position = endRodPosition;
            rod.transform.rotation = endRodRotation;
        }
    }

    private IEnumerator ThrowRod()
    {
        yield return new WaitForEndOfFrame();
        float elapsedTime = 0f;
        while (elapsedTime < lerpDuration)
        {
            if (rod != null)
            {
                rod.transform.position = Vector3.Lerp(endRodPosition, startRodPosition, elapsedTime / lerpDuration);
                rod.transform.rotation = Quaternion.Lerp(endRodRotation, startRodRotation, elapsedTime / lerpDuration);
            }
            elapsedTime += Time.deltaTime * 2;
            yield return null;
        }
        if (rod != null)
        {
            rod.transform.position = startRodPosition;
            rod.transform.rotation = startRodRotation;
        }
        if (!CanBeCatched)
        {
            StartCoroutine(CastBait());
        }
    }

    private IEnumerator CastBait()
    {

        yield return new WaitForEndOfFrame();
        if (crosshairScript != null)
        {
            Vector3 targetPosition = crosshairScript.Stop();
            targetPosition = new Vector3(targetPosition.x, -0.9f, targetPosition.z);
            baitGameObject = Instantiate(baitPrefab, BaitSpawnPosition, BaitSpawnRotation);
            StartCoroutine(MoveBaitToTarget(baitGameObject.transform, targetPosition));
            isCasted = true;
        }
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
            baitTransform.rotation = Quaternion.Lerp(BaitSpawnRotation, Quaternion.Euler(0, 0, 0), journey);
            yield return null;
        }
        isCasted = false;
    }
}