using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class SkiTrackscript : MonoBehaviour
{
    public bool isRotating;
    public bool isSnow;
    public float rotationSpeed = 30f;
    private GameObject menuhandler;
    private void Start(){
        menuhandler= GameObject.Find("InGameMenuHandler");
    }
    private void Update ()
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
