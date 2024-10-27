using UnityEngine;

public class PingPongScript : MonoBehaviour
{
    public float amplitude = 1f;  // Height of the oscillation
    public float speed = 1f;      // Speed of the oscillation
    private float newY;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new y position using PingPong
        newY = startPosition.y + Mathf.PingPong(Time.time * speed, amplitude * 2) - amplitude;
        // Update the object's position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
