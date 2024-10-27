using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Animator animator;
    private Rigidbody rb;
    private Vector3 movement;
    public stateHandler stateHandler;
    private bool isSFXPlaying = false;
    private bool stepturn;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (stateHandler.isPaused == false && stateHandler.isCompleted == false)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
            animator.SetFloat("walkSpeed", movement.magnitude * moveSpeed);
            if (movement.magnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                if ((Mathf.Abs(moveHorizontal)+Mathf.Abs(moveVertical)) > 0 && !isSFXPlaying)
                {
                    StartCoroutine(PlayStep());
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private IEnumerator PlayStep()
    {
        isSFXPlaying = true;

        if (stepturn == true)
        {
            FindObjectOfType<SoundManager>().Play("Grass1");
            stepturn = !stepturn;
        }
        else
        {
            FindObjectOfType<SoundManager>().Play("Grass2");
            stepturn = !stepturn;
        }
        yield return new WaitForSeconds(0.60f);
        isSFXPlaying = false;
    }
}
