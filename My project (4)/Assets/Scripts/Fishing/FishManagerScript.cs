using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishManagerScript : MonoBehaviour
{
    public GameObject Fish1Lay;
    public GameObject Fish2Lay;
    public GameObject Fish3Lay;
    public GameObject Fish1Fly;
    public GameObject Fish2Fly;
    public GameObject Fish3Fly;
    private GameObject FishObject;
    public bool fishMoved = false;
    private int CurrentFish;
    private float journey;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private CielScript cielScript;
    private Fishing fishing;
    public string NextSceneName;
    private stateHandler stateHandler;
    void Start()
    {
        Fish1Lay.SetActive(false);
        Fish2Lay.SetActive(false);
        Fish3Lay.SetActive(false);
        CurrentFish = 1;
        fishing = FindObjectOfType<Fishing>();

    }
    public void SwitchFish()
    {
        switch (CurrentFish)
        {
            case 1: FishObject = Fish1Fly; break;
            case 2: FishObject = Fish2Fly; break;
            case 3: FishObject = Fish3Fly; break;
            default: break;
        }
    }
    public void SwitchFishLay()
    {
        if (SoundManager.sndm != null){SoundManager.sndm.Play("FishDrop");}
        switch (CurrentFish)
        {
            case 1: Fish1Lay.SetActive(true); break;
            case 2: Fish2Lay.SetActive(true); break;
            case 3: Fish3Lay.SetActive(true); break;
            default: break;
        }
        CurrentFish++;
    }
    public void ExternalFishMovement()
    {
        fishMoved = false;
        StartCoroutine(FishMovement());
    }
    private IEnumerator FishMovement()
    {
        switch (CurrentFish)
        {

            case 1:
                journey = 0;
                cielScript = FindObjectOfType<CielScript>();
                Vector3 startPosition = Fish1Fly.transform.position;
                Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - 3.5f);
                while (journey <= 1)
                {
                    journey += Time.deltaTime / 4;
                    Fish1Fly.transform.position = Vector3.Lerp(startPosition, targetPosition, journey);
                    if (cielScript != null)
                    {
                        cielScript.continueThrow();
                        Destroy(cielScript);
                    }
                    yield return null;
                }
                SwitchFishLay();
                fishMoved = true; break;
            case 2:
                journey = 0;
                cielScript = FindObjectOfType<CielScript>();
                startPosition = Fish2Fly.transform.position;
                targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - 3.5f);
                while (journey <= 1)
                {
                    journey += Time.deltaTime / 4;
                    Fish2Fly.transform.position = Vector3.Lerp(startPosition, targetPosition, journey);
                    if (cielScript != null)
                    {
                        cielScript.continueThrow();
                        Destroy(cielScript);
                    }
                    yield return null;
                }
                SwitchFishLay();
                fishMoved = true; break;
            case 3:
                // SceneManager.LoadScene("ShootingRange");
                StartCoroutine(StartNewScene());
                // stateHandler.isCompleted = true;
                journey = 0;
                cielScript = FindObjectOfType<CielScript>();
                startPosition = Fish3Fly.transform.position;
                targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - 5.5f);
                while (journey <= 1)
                {
                    journey += Time.deltaTime / 4;
                    Fish3Fly.transform.position = Vector3.Lerp(startPosition, targetPosition, journey);
                    if (cielScript != null)
                    {
                        cielScript.stopThrow();
                        Destroy(cielScript);
                    }
                    yield return null;
                }
                SceneManager.LoadScene(NextSceneName);
                fishing.stateHandler.isCompleted = true;
                SwitchFishLay();
                fishMoved = true; break;
            default: break;
        }
        yield return null;
    }
    private IEnumerator StartNewScene()
    {
        yield return new WaitForSeconds(6.5f);
        SceneManager.LoadScene(NextSceneName);
        fishing.stateHandler.isCompleted = true;
        yield return null;
    }
}
