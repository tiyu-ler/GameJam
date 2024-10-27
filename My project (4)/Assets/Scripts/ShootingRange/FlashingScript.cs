using UnityEngine;

public class Flasher : MonoBehaviour
{
    public GameObject projectile;
    public float DestroyTime;
    public bool calculateCamDistance = false;
    public stateHandler stateHandler;

    void Update()
    {
        if (stateHandler.isPaused == false && stateHandler.isCompleted == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (projectile)
                {
                    GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward * (calculateCamDistance ? 1.2f : 0), transform.rotation);
                    Destroy(newProjectile, DestroyTime);
                }
            }
        }
    }
}