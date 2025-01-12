using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ConstellationScript : MonoBehaviour
{
    public GameObject doneVersion, controller;
    private bool istriggered;
    
    private void Start()
    {
        istriggered = false;
        doneVersion.SetActive(false);
    }
    public void triggerConst()
    {
        if (istriggered == false)
        {
            doneVersion.SetActive(true);
            Destroy(gameObject.GetComponent<BoxCollider>());
            controller.GetComponent<constellationController>().SpotCount += 1;
            istriggered=true;
        }
    }
}
