using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CielScript : MonoBehaviour
{
    private GameObject Bait;
    private Fishing fishing;
    private bool isBaitCaught = false;
    public bool CanBeCatched = false;
    public GameObject Fish1Lay;
    public GameObject Fish2Lay;
    public GameObject Fish3Lay;
    public GameObject Fish1Fly;
    public GameObject Fish2Fly;
    public GameObject Fish3Fly;
    public GameObject FishObject;
    private FishManagerScript fishManager;
    private GameObject cantCatch;
    void Start()
    {
        fishManager = FindObjectOfType<FishManagerScript>();
        fishing = FindObjectOfType<Fishing>();
    }
    void Update()
    {
        if (Bait != null)
        {
            fishing.CanBeCatched = true;
            if (Bait.transform.position.y <= -0.89f && !isBaitCaught)
            {
                StartCoroutine(FishBait());
            }
            if (Input.GetKeyDown(KeyCode.Space) && CanBeCatched)
            {
                fishing.ExternalRaise();
                
                fishManager.ExternalFishMovement();
                // Debug.Log("Ok 1");
                // if (fishManager.fishMoved)
                // {
                    // Debug.Log("Ok");
                    // fishManager.SwitchFishLay();
                    // fishing.ExternalThrow();
                    // CanBeCatched = false;
                    // isBaitCaught = false;
                    // fishing.Destroy();
                    // fishing.NewToss();
                    // Destroy(transform.parent.gameObject);
                // }
            }
        }
    }
    public void continueThrow()
    {
        CanBeCatched = false;
        isBaitCaught = false;
        fishing.Destroy();
        fishing.NewToss();
        fishing.ExternalThrow();
        Destroy(transform.parent.gameObject);
    }
    public void stopThrow()
    {
        CanBeCatched = false;
        isBaitCaught = false;
        fishing.Destroy();
        fishing.NewToss();
        fishing.StopFishing();
        SceneManager.LoadScene("ShootingRange");
        Destroy(transform.parent.gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "bait")
        {
            Bait = other.gameObject;
        }
    }

    private IEnumerator FishBait()
    {
        if (!isBaitCaught)
        {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f,3f));
        float journey = 0;
        Vector3 startPosition = Bait.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, -1.15f, startPosition.z);
        while (journey <= 1)
        {
            journey += Time.deltaTime * 10;
            Bait.transform.position = Vector3.Lerp(startPosition, targetPosition, journey);
            yield return null;
        }
        journey = 0;
        while (journey <= 1)
        {
            journey += Time.deltaTime * 10;
            Bait.transform.position = Vector3.Lerp(Bait.transform.position, startPosition, journey);
            yield return null;
        }
        isBaitCaught = true;
        CanBeCatched = true;
        }
        yield return null;
    }
}
