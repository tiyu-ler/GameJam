using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TelescopeInteractScript : MonoBehaviour
{
    public GameObject firstPack;           // Location, telescope, light, etc.
    public GameObject secondPack;          // Starry sky
    public GameObject interactionUI;       // UI element that says "Press Space"
    public Transform player;               // Reference to the player
    public GameObject outlineObject;
    public bool changedScene;
    private CameraFollow cameraFollowScript; // Reference to the CameraFollow script
    private bool isPlayerInRange = false;    // Flag to check if the player is within the trigger
    public stateHandler stateHandler;
    private void Start()
    {
        changedScene = false;
        firstPack.SetActive(true);
        interactionUI.SetActive(false);
        outlineObject.SetActive(false);
        cameraFollowScript = Camera.main.GetComponent<CameraFollow>();
    }

    private void Update()
    {
        if (stateHandler.isPaused == false && stateHandler.isCompleted == false)
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
            {
                if (Random.Range(0f, 1f) > 0.5f)
                    SoundManager.sndm.Play("Constelation_low_pitch");
                else
                    SoundManager.sndm.Play("Constelation_high_pitch");
                changedScene = true;
                firstPack.SetActive(false);
                outlineObject.SetActive(true);
                cameraFollowScript.IsMoved = false;
                interactionUI.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionUI.SetActive(true); // Show interaction UI
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exited the trigger
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionUI.SetActive(false); // Hide interaction UI
        }
    }
}
