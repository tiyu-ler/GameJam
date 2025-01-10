using System;
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
    public bool inv_x, inv_y;
    private int invXval, invYval;
    public string WalkSFXTitle1,WalkSFXTitle2;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (inv_x == true)
        {
            invXval = -1;
        }
        else { invXval = 1; }
        if (inv_y == true)
        {
            invYval = -1;
        }
        else { invYval = 1; }
    }

    private void Update()
    {
        if (stateHandler.isPaused == false && stateHandler.isCompleted == false)
        {
            float moveHorizontal = invXval*Input.GetAxis("Horizontal");
            float moveVertical = invYval*Input.GetAxis("Vertical");
            movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
            animator.SetFloat("walkSpeed", movement.magnitude * moveSpeed);
            if (movement.magnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                if ((Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical)) > 0 && !isSFXPlaying && SoundManager.sndm != null)
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
            FindObjectOfType<SoundManager>().Play(WalkSFXTitle1);
            stepturn = !stepturn;
        }
        else
        {
            FindObjectOfType<SoundManager>().Play(WalkSFXTitle2);
            stepturn = !stepturn;
        }
        yield return new WaitForSeconds(0.60f);
        isSFXPlaying = false;
    }
}
