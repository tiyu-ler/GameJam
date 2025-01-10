using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    public stateHandler stateHandler;
    public int maxXrotation;
    private float cameraVerticalRotation = 0f;
    private float playerHorizontalRotation = 0f;

    void Start()
    {
        playerHorizontalRotation = player.localEulerAngles.y;
        cameraVerticalRotation = player.localEulerAngles.x;
        UpdateCursorState();
    }
    void Update()
    {
        if (!stateHandler.isPaused && !stateHandler.isCompleted)
        {
            HandleMouseInput();
        }
        UpdateCursorState();
    }

    private void HandleMouseInput()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -70f, 50f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        playerHorizontalRotation += inputX;
        playerHorizontalRotation = Mathf.Clamp(playerHorizontalRotation, -maxXrotation+90, maxXrotation+90); 
        player.localEulerAngles = Vector3.up * playerHorizontalRotation;
    }

    private void UpdateCursorState()
    {
        if (stateHandler.isPaused || stateHandler.isCompleted)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
