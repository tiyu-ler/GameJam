using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public float power = 10.0f;
    public float DestroyTime;
    public bool useRigidbody = false, calculateCamDistance = false;
    public LayerMask hitLayers;
    public stateHandler stateHandler;

    void Update()
    {
        if (stateHandler.isPaused == false && stateHandler.isCompleted == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (projectile)
                {
                    Ray ray = new Ray(transform.position, transform.forward);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayers))
                    {
                        //Debug.Log("Hit: " + hit.collider.name);

                        if (hit.collider.gameObject.CompareTag("Target"))
                        {
                            hit.collider.gameObject.GetComponent<Target>().KillTarget();
                        }
                    }

                    GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward * (calculateCamDistance ? 1.2f : 0), transform.rotation * Quaternion.Euler(0, 90, 90));
                    Destroy(newProjectile, DestroyTime);

                    if (useRigidbody)
                    {
                        if (!newProjectile.GetComponent<Rigidbody>())
                            newProjectile.AddComponent<Rigidbody>();

                        newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.VelocityChange);
                    }
                    if (SoundManager.sndm != null)
                    {
                        SoundManager.sndm.Play("CartoonShot");
                    }
                }
            }
        }
    }
}
