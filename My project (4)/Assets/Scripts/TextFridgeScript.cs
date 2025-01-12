using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextFridgeScript : MonoBehaviour
{
    public GameObject interactionUI;       // UI element that says "Press Space"
    public GameObject Exclamation;
    public Transform player;
    public string NextSceneName;
    private bool isPlayerInRange = false;
   // private SceneLoader sceneLoader;

    void Start()
    {
        interactionUI.SetActive(false);
      //  sceneLoader = FindObjectOfType<SceneLoader>();
    }
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
          //  Destroy(Exclamation);
            Debug.Log("Player read text");
          //  sceneLoader.LoadNextScene();
          SceneManager.LoadScene(NextSceneName);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionUI.SetActive(false);
        }
    }
}
