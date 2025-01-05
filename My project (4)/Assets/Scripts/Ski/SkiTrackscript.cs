using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkiTrackscript : MonoBehaviour
{
    public bool isRotating;
    public bool isSnow;
    public float rotationSpeed = 30f;
    void Update ()
    {
        if (isRotating )
        {
            if (!isSnow)
            {
                transform.Rotate (rotationSpeed*Time.deltaTime,0,0);
            }
            else
            {
                transform.Rotate (-rotationSpeed*Time.deltaTime,0,0);
            }
        }
    }
}
