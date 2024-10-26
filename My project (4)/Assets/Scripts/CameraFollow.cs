using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           // The player's transform to follow
    public Transform Camera;
    public Vector3 offset = new Vector3(0, 3.7f, -2.4f);  // Offset position from the player
    public float smoothSpeed = 0.125f; // Smooth factor for the camera's movement
    public bool IsMoved;
    public float mouseSensitivity = 300f;
    private float verticalRotation = 0f;
    void Start()
    {
        IsMoved = true;
    }
    private void LateUpdate()
    {
        if (player != null && IsMoved)
        {
            // Calculate desired position based on player's position and offset
            Vector3 desiredPosition = player.position + offset;

            // Smoothly interpolate between the camera's current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply the smoothed position to the camera
            transform.position = smoothedPosition;

            // Optionally, make the camera look at the player
            transform.LookAt(player);
        }
        else
        {
            RotateCamera();
        }
    }
    void RotateCamera()
    {
        Camera.position = new Vector3(0,0,0);
        float mouseX = Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Vertical") * mouseSensitivity * Time.deltaTime;
        Camera.transform.Rotate(Vector3.up * mouseX);
        Camera.transform.Rotate(Vector3.left * mouseY);
        // Camera.localRotation = Quaternion.Euler(Math.Clamp(verticalRotation, -80, 33), 0f, 0f); 
    }
}
