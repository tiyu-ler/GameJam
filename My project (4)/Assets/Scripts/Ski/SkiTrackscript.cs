using UnityEngine;

public class SkiTrackscript : MonoBehaviour
{
    public bool isRotating;
    public bool isSnow;
    public float rotationSpeed = 30f;
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
