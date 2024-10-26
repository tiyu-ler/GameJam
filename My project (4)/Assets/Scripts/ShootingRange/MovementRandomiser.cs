using UnityEngine;

public class RandomizeTargetMovement : MonoBehaviour
{
    private TargetMovementScript targetMovement;
    void Start()
    {
        targetMovement = GetComponent<TargetMovementScript>();

        if (targetMovement != null)
        {
            targetMovement.speed = Random.Range(0.5f, 3.0f);
            targetMovement.amplitude = Random.Range(0.5f, 3.0f);

            targetMovement.Xaxis = Random.value > 0.5f;
            targetMovement.Yaxis = Random.value > 0.5f;
            targetMovement.Zaxis = Random.value > 0.5f;

            targetMovement.XFunction = (TargetMovementScript.AxisFunction)Random.Range(0, 2); // 0: Sin, 1: Cos
            targetMovement.YFunction = (TargetMovementScript.AxisFunction)Random.Range(0, 2);
            targetMovement.ZFunction = (TargetMovementScript.AxisFunction)Random.Range(0, 2);

            targetMovement.invertX = Random.value > 0.5f;
            targetMovement.invertY = Random.value > 0.5f;
            targetMovement.invertZ = Random.value > 0.5f;
        }
    }
}
