using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform Camera;
    public List<GameObject> stars; 
    public RawImage outline;
    
    public Color defaultColor = Color.white;   // Default color when no star is targeted
    private Color closeColor = Color.green;
    private Color midColor = Color.yellow;
    private Color farColor = Color.red;

    private float maxDistance = 0.5f;          // Distance threshold for the green outline
    private float midDistance = 1.5f;          // Distance threshold for the yellow outline
    private GameObject currentStar;
    public float tolerance = 0.1f;             // Tolerance for crosshair positioning
    public Vector3 offset = new Vector3(0, 3.7f, -2.4f);  // Offset position from the player
    public float smoothSpeed = 0.125f;         // Smooth factor for the camera's movement
    public bool IsMoved;
    public float mouseSensitivity = 300f;

    private void Start()
    {
        IsMoved = true;
        if (outline != null) outline.color = defaultColor;
    }

    private void Update()
    {
        currentStar = FindClosestStar();

        if (currentStar != null)
        {
            UpdateOutlineAndLight();
            CheckShoot();
        }
        else
        {
            outline.color = defaultColor; // Set to default when no star is within tolerance
        }
    }

    private GameObject FindClosestStar()
    {
        GameObject closestStar = null;
        float minDistance = float.MaxValue;

        foreach (GameObject star in stars)
        {
            Vector3 screenPoint = Camera.GetComponent<Camera>().WorldToScreenPoint(star.transform.position);
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            float distance = Vector2.Distance(screenPoint, screenCenter);

            if (distance < minDistance && distance <= midDistance + tolerance)
            {
                minDistance = distance;
                closestStar = star;
            }
        }

        return closestStar;
    }

    private void UpdateOutlineAndLight()
    {
        Vector3 screenPoint = Camera.GetComponent<Camera>().WorldToScreenPoint(currentStar.transform.position);
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        float distance = Vector2.Distance(screenPoint, screenCenter);

        if (distance < maxDistance + tolerance)
        {
            outline.color = closeColor;
        }
        else if (distance < midDistance + tolerance)
        {
            outline.color = midColor;
        }
        else
        {
            outline.color = farColor;
        }
    }

    private void CheckShoot()
    {
        if (outline.color == closeColor && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shooted Stars");
        }
    }

    private void LateUpdate()
    {
        if (player != null && IsMoved)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(player);
        }
        else
        {
            RotateCamera();
        }
    }

    void RotateCamera()
    {
        Camera.position = new Vector3(0, 0, 0);
        float mouseX = Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Vertical") * mouseSensitivity * Time.deltaTime;
        Camera.transform.Rotate(Vector3.up * mouseX);
        Camera.transform.Rotate(Vector3.left * mouseY);
    }
}
