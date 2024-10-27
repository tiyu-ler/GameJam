using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public float sprintSpeed = 6.0f;
    public float gravity = 9.81f;
    private CharacterController myController;
    public stateHandler stateHandler;

    void Start()
    {
        myController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (stateHandler.isPaused == false && stateHandler.isCompleted == false)
        {
            float movementY = Input.GetAxis("Vertical");
            float movementX = Input.GetAxis("Horizontal");

            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            Vector3 move = transform.right * movementX + transform.forward * movementY;

            myController.Move(move * (isRunning ? sprintSpeed : movementSpeed) * Time.deltaTime);
        }
    }
}
