using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private Rigidbody rb;
    private Vector3 movement;
    private bool isSFXPlaying = false;
    private bool stepturn;


    [Header("Dependencies")]
    public Transform characterVisual;
    public Transform camerapos;
    public Animator animator;
    public string WalkSFXTitle1, WalkSFXTitle2;
    public stateHandler stateHandler;

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

            Vector3 forward = camerapos.forward;
            Vector3 right = camerapos.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            movement = (forward * moveVertical + right * moveHorizontal).normalized;

            if (movement.magnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                characterVisual.rotation = Quaternion.Slerp(
                    characterVisual.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

                if (!isSFXPlaying&&SoundManager.sndm != null)
                {
                    StartCoroutine(PlayStep());
                }
            }

            animator.SetFloat("walkSpeed", movement.magnitude * moveSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (movement.magnitude > 0)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
    private IEnumerator PlayStep()
    {
        isSFXPlaying = true;
        FindObjectOfType<SoundManager>().Play(stepturn ? WalkSFXTitle1 : WalkSFXTitle2);
        stepturn = !stepturn;
        yield return new WaitForSeconds(0.6f);
        isSFXPlaying = false;
    }
}
