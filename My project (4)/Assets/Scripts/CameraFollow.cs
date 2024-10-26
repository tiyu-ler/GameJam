using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform Camera;
    private Camera mainCamera;
    public List<GameObject> stars; 
    public List<GameObject> Boxes; 
    public List<Material> finishedStars; 
    public RawImage outline;
    // public float MinFOV;
    // public float MaxFOV;
    // public float ChangeFovWithDistance = 10f;
    public GameObject interactionUI;
    public Color defaultColor = Color.white;   // Default color when no star is targeted
    private Color orangeColor = Color.yellow;
    private Color greenColor = Color.green;

    public Vector3 offset = new Vector3(0, 3.7f, -2.4f);  // Offset position from the player
    public float smoothSpeed = 0.125f;         // Smooth factor for the camera's movement
    public bool IsMoved;
    public float mouseSensitivity = 300f;
    public float raycastRange = 500f;          // Max distance for raycast to detect stars
     private TelescopeInteractScript telescopeInteract;
    private GameObject currentStar;
    private bool isStarsCloseActive;
    private float targetFOV;
    private void Start()
    {
        mainCamera = Camera.GetComponent<Camera>();
        telescopeInteract = FindObjectOfType<TelescopeInteractScript>();
        interactionUI.SetActive(false);
        currentStar = null;
        IsMoved = true;
        outline.color = defaultColor;
        // targetFOV = MaxFOV;
        isStarsCloseActive = GameObject.FindWithTag("StarsClose") != null && GameObject.FindWithTag("StarsClose").activeInHierarchy;
    }

    void Update()
    {
        // AdjustFovWithDistance();
        // Update `isStarsCloseActive` status
        isStarsCloseActive = GameObject.FindWithTag("StarsClose") != null && GameObject.FindWithTag("StarsClose").activeInHierarchy;
        
        // Set default color
        outline.color = defaultColor;

        // Raycast from camera's center towards stars
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            if (hit.collider.CompareTag("StarsClose"))
            {
                currentStar = hit.collider.gameObject;

                if (isStarsCloseActive)
                {
                    outline.color = orangeColor;
                }
            }
            if (hit.collider.CompareTag("Stars"))
            {
                interactionUI.SetActive(true);
                currentStar = hit.collider.gameObject;
                if (isStarsCloseActive)
                {
                    outline.color = greenColor; // Set to orange if within the StarsClose range
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        CheckShoot();
                    }
                    
                }
            }
            else
            {
                if (telescopeInteract != null && telescopeInteract.changedScene)
                {
                    interactionUI.SetActive(false);
                }
            }
        }
        else
        {
            currentStar = null; // No star detected by the raycast
        }
    }

    private void CheckShoot()
    {
        int starID = int.Parse(currentStar.name) - 1;
        {
            Renderer starRenderer = stars[starID].GetComponent<Renderer>();
            starRenderer.material = finishedStars[starID];
            stars[starID].tag = "finished";
            Boxes[starID].SetActive(false);
            Boxes[starID+1].SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (player != null && IsMoved)
        {
	            transform.LookAt(player.transform);
                // AdjustFovWithDistance();
        }
        else
        {
            RotateCamera();
        }
    }
    // private void AdjustFovWithDistance()
    // {
    //     float distance = Vector3.Distance(Camera.position, player.position);
    //     float clampedDistance = Mathf.Clamp(distance, 10f, 20f);
    //     float normalizedDistance = ;
    //     Mathf.Lerp(MinFOV, MaxFOV, Time.deltaTime);
    // }
    void RotateCamera()
    {
        Camera.position = new Vector3(0, 0, 0);
        float mouseX = Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Vertical") * mouseSensitivity * Time.deltaTime;
        Camera.transform.Rotate(Vector3.up * mouseX);
        Camera.transform.Rotate(Vector3.left * mouseY);
    }
}
