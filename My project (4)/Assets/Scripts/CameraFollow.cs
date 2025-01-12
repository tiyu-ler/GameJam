using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    private Transform Camera;
    public Transform player;
    public Vector3 offset;
    public GameObject scope, telescope;
    public stateHandler stateHandler;
    public int FollowSpeed, ScopeSpeed;
    private Vector3 startCamPos;
    public bool isUsingScope;
    public float rayRange = 100f;
    private int CrosshairColPick;

    private GameObject detectedObject;

    private void Start()
    {
        Camera = gameObject.transform;
        startCamPos = transform.position;
    }

    void Update()
    {
        if (isUsingScope)
        {
            
            if (Input.GetKeyDown(KeyCode.Space)&&detectedObject != null)
            {
                Debug.Log("1");
                detectedObject.GetComponent<ConstellationScript>().triggerConst();
                if (SoundManager.sndm != null)
                {
                    if (Random.Range(0f, 1f) > 0.5f)
                        SoundManager.sndm.Play("Constelation_low_pitch");
                    else
                        SoundManager.sndm.Play("Constelation_high_pitch");
                }
            }
        }
        //if (isUsingScope && Input.GetKeyDown(KeyCode.Escape)))
       // {
       //     isUsingScope = false;
       //     telescope.GetComponent<TelescopeInteractScript>().RemoveTelescopeUi();
       // }
       Debug.Log(detectedObject);
    }

    void LateUpdate()
    {
        if (!isUsingScope)
            CamFollow();
        else
        {
            RotateCamera();
            DetectObject();
        }
    }

    void RotateCamera()
    {
        Camera.position = new Vector3(0, 0, 0);
        float mouseX = Input.GetAxis("Horizontal") * ScopeSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Vertical") * ScopeSpeed * Time.deltaTime;
        Camera.transform.Rotate(Vector3.up * mouseX);
        Camera.transform.Rotate(Vector3.left * mouseY);
    }

    void CamFollow()
    {
        Vector3 desiredPosition = startCamPos + (player.position * offset.magnitude);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, FollowSpeed);

        transform.position = smoothedPosition;
        transform.LookAt(player);
    }

    void DetectObject()
    {
        Ray ray = new Ray(Camera.position, Camera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayRange))
        {
            if (hit.collider.gameObject.CompareTag("Constellation") && hit.collider is BoxCollider)
            {
                if (CrosshairColPick != 1) { ScopeCollor(1); }
                if (detectedObject == null) { detectedObject = hit.collider.gameObject;}
                
            }
            else
            {
                Debug.Log("11");
                detectedObject = null;
                if (CrosshairColPick != 0) { ScopeCollor(0); }
            }
        }
        else
        {
            Debug.Log("122");
            detectedObject = null;
            if (CrosshairColPick != 0) { ScopeCollor(0); }
        }
    }
    void ScopeCollor(int a)
    {
        RawImage img = scope.GetComponent<RawImage>();
        switch (a)
        {
            case (1):
                img.color = Color.blue;
                CrosshairColPick = 1;
                break;
            case (0):
                img.color = Color.white;
                CrosshairColPick = 0;
                break;
        }
    }
}
