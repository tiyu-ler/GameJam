using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSkiScript : MonoBehaviour
{
    private int CurrentRow;
    private Vector3 targetPosition;
    public GameObject track;
    public GameObject WinScreen;
    public GameObject DeathScreen;
    private float lerpSpeed = 7f;
    private Animator animator;
    private bool canTurn = true;
    public float turnCooldown = 0.3f;
    public SkiSpawner skiSpawner;
    private bool isDead;
    private bool canBeDead;
    private GameObject[] obstacles;
    public stateHandler stateHandler;
    void Start()
    {
        isDead = false;
        canBeDead = true;
        DeathScreen.SetActive(false);
        WinScreen.SetActive(false);
        CurrentRow = 3;
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        animator.speed = 1;
        canTurn = true;
        StartCoroutine(Win());
        if (SoundManager.sndm != null)
        {
            SoundManager.sndm.Play("SkiingTheme");
            SoundManager.sndm.Play("Snow");
        }
    }
    void Update()
    {
        if (stateHandler.isCompleted == false&&SoundManager.sndm != null)
        {
            
            if (stateHandler.isPaused == true && SoundManager.sndm.IsPlaying("Snow") == true)
            {
                SoundManager.sndm.Stop("Snow");
            }
            else if (stateHandler.isPaused == false && SoundManager.sndm.IsPlaying("Snow") == false)
            {
                SoundManager.sndm.Play("Snow");
            }
        }
        if (stateHandler.isPaused == false && stateHandler.isCompleted == false)
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
                        if (SoundManager.sndm != null) { SoundManager.sndm.Play("SkiiStep"); }
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
                        if (SoundManager.sndm != null) { SoundManager.sndm.Play("SkiiStep"); }
                    }
                }
            }
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
        }
    }
    private IEnumerator LockTurnInput()
    {
        canTurn = false;
        yield return new WaitForSeconds(turnCooldown);
        animator.SetInteger("Turn", 3);
        canTurn = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            Death();
        }
    }
    public void Death()
    {
        if (canBeDead)
        {
            isDead = true;
            animator.speed = 0;
            canTurn = false;
            skiSpawner.IsSpawning = false;
            StopRotating();
            StartCoroutine(RestartTimer());
        }
    }
    private void StopRotating()
    {
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        track.GetComponent<SkiTrackscript>().isRotating = false;
        foreach (GameObject obstacle in obstacles)
        {
            SkiTrackscript skiTrackscript = obstacle.GetComponent<SkiTrackscript>();
            if (skiTrackscript != null)
            {
                skiTrackscript.isRotating = false;
                canTurn = false;
            }
        }
    }
    private IEnumerator Win()
    {
        yield return new WaitForSeconds(45);
        skiSpawner.IsSpawning = false;
        yield return new WaitForSeconds(7.5f);
        if (!isDead)
        {
            canBeDead = false;
            canTurn = false;
            // skiSpawner.IsSpawning = false;
            // StopRotating();
            animator.SetInteger("Turn", 2);
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, 240f, transform.eulerAngles.z);
            animator.speed = 0.4f;
            float duration = 1.05f;
            float elapsedTime = 0f;
            obstacles = GameObject.FindGameObjectsWithTag("obstacle");
            while (elapsedTime < duration)// wtf stepbro
            {
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                foreach (GameObject obstacle in obstacles)
                {
                    try
                    {
                        SkiTrackscript skiTrackscript = obstacle.GetComponent<SkiTrackscript>();
                        if (skiTrackscript != null)
                        {
                            skiTrackscript.rotationSpeed = Math.Max(skiTrackscript.rotationSpeed - 0.08f, 0);
                        }
                    }
                    catch { }
                }
                track.GetComponent<SkiTrackscript>().rotationSpeed = Math.Max(track.GetComponent<SkiTrackscript>().rotationSpeed - 0.08f, 0);
                yield return null;
            }
            StopRotating();
            transform.rotation = targetRotation;
            // yield return new WaitForSeconds(0.42f);
            animator.speed = 0;
            yield return new WaitForSeconds(0.5f);
            WinScreen.SetActive(true);
            SoundManager.sndm.StopAllSounds();
            SoundManager.sndm.Play("Fanfare");
            stateHandler.isCompleted = true;
            yield return new WaitForSeconds(3.5f);
            SoundManager.sndm.StopAllSounds();
            SceneManager.LoadScene("MainMenu");
        }
    }
    private IEnumerator RestartTimer()
    {
        yield return new WaitForSeconds(0.5f);
        DeathScreen.SetActive(true);
        stateHandler.isCompleted = true;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Level_4(Snow)");
    }
    public void ResetTurn()
    {
        animator.SetInteger("Turn", 3);
    }
}
