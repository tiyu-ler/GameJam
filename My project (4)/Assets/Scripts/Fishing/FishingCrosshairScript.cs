using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCrosshairScript : MonoBehaviour
{
    private float actualSpeed;
    public float speed = 5f;           // Speed of movement
    public float maxXPosition;  // Maximum position on the Z axis
    public float minXPosition; // Minimum position on the Z axis
    
    void Start()
    {
        actualSpeed = 0;
    }
    public void LetMove()
    {
        actualSpeed = speed;
    }

    void Update()
    {
        float xPosition = Mathf.PingPong(Time.time * actualSpeed, maxXPosition - minXPosition) + minXPosition;
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }
    public Vector3 Stop()
    {
        actualSpeed = 0;
        return transform.position;
    }
}
