using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkiScript : MonoBehaviour
{
    private int CurrentRow;
    private Vector3 targetPosition;
    private float lerpSpeed = 7f;
    private Animator animator;
    private bool canTurn = true;
    public float turnCooldown = 0.3f;
    void Start()
    {
        CurrentRow = 3;
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canTurn)
        {
            // A is left
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (CurrentRow > 1)
                {
                    CurrentRow -= 1;
                    targetPosition += new Vector3(1.8f, 0f, 0f);
                    animator.SetInteger("Turn", 1); // 1 - Left, 2 - Right; 3 - Go
                    StartCoroutine(LockTurnInput());
                }
            }

            // D is right
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (CurrentRow < 5)
                {
                    CurrentRow += 1;
                    targetPosition += new Vector3(-1.8f, 0f, 0f);
                    animator.SetInteger("Turn", 2); // 1 - Left, 2 - Right; 3 - Go
                    StartCoroutine(LockTurnInput());
                }
            }
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
    }

    private IEnumerator LockTurnInput()
    {
        canTurn = false;
        yield return new WaitForSeconds(turnCooldown);
        animator.SetInteger("Turn", 3);
        canTurn = true;
    }

    public void ResetTurn()
    {
        animator.SetInteger("Turn", 3);
    }
}
