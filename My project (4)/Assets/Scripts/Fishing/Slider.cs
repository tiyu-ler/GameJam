using UnityEngine;

public class Slider : MonoBehaviour
{
    public float speed = 5f;           // Speed of movement
    public float maxZPosition = 270f;  // Maximum position on the Z axis
    public float minZPosition = -278f; // Minimum position on the Z axis

    void Update()
    {
        // Calculate the new Z position using Mathf.PingPong
        float zPosition = Mathf.PingPong(Time.time * speed, maxZPosition - minZPosition) + minZPosition;

        // Set the object's position, preserving its X and Y positions
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    // Optional method to stop the movement
    public void Stop()
    {
        speed = 0;
    }
}
