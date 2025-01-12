using UnityEngine;
using Cinemachine;

public class ThirdPersonCamController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera; // Assign this in the inspector
    public float cameraRotationSpeed = 100f; // Speed at which the camera rotates
    public Transform player;                 // Reference to the player Transform

    private void Update()
    {
        // Get input for rotating the camera (arrow keys or assigned keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Adjust camera's X-axis bias based on horizontal input
        if (horizontalInput != 0)
        {
            freeLookCamera.m_XAxis.Value += horizontalInput * cameraRotationSpeed * Time.deltaTime;
        }
    }
}
