using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public float speed = 5f; 
    private Vector3 startPosition;
    public float resetPosition = -4f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    { 
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        if (transform.position.x <= resetPosition)
        {
            transform.position = startPosition;
        }
    }
    public void Stop(){
        speed = 0; 
    }
}
