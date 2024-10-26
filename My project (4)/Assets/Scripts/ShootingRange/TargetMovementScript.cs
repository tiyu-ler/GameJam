using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovementScript : MonoBehaviour
{
    public enum AxisFunction { Sin, Cos }
    public float speed = 1.0f;
    public float amplitude = 1.0f;
    public bool Xaxis = true;
    public bool Yaxis = false;
    public bool Zaxis = true;
    public AxisFunction XFunction = AxisFunction.Sin;
    public AxisFunction YFunction = AxisFunction.Cos;
    public AxisFunction ZFunction = AxisFunction.Sin;
    public bool invertX = false;
    public bool invertY = false;
    public bool invertZ = false;

    private Vector3 initialPosition;
    private Rigidbody rb;
    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true; 
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return; 

        float time = Time.time * speed;
        float x = Xaxis ? amplitude * useMathFunction(XFunction, time) : 0;
        float y = Yaxis ? amplitude * useMathFunction(YFunction, time) : 0;
        float z = Zaxis ? amplitude * useMathFunction(ZFunction, time) : 0;

        Vector3 newPosition = initialPosition + new Vector3(invertX ? -x : x, invertY ? -y : y, invertZ ? -z : z);

        rb.MovePosition(newPosition);
    }

    private float useMathFunction(AxisFunction func, float time)
    {
        switch (func)
        {
            case AxisFunction.Sin:
                return Mathf.Sin(time);
            case AxisFunction.Cos:
                return Mathf.Cos(time);
        }
        return 0;
    }
}
