using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectile;
    public float power = 10.0f;
    public AudioClip shootSFX;
    public float DestroyTime;
    public bool useRigidbody = false, calculateCamDistance = false;
    public LayerMask hitLayers;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (projectile)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayers))
                {
                    Debug.Log("Hit: " + hit.collider.name);

                    if (hit.collider.gameObject.CompareTag("Target"))
                    {
                        Destroy(hit.collider.gameObject);
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
                
                if (shootSFX)
                {
                    if (newProjectile.GetComponent<AudioSource>())
                    {
                        newProjectile.GetComponent<AudioSource>().PlayOneShot(shootSFX);
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(shootSFX, newProjectile.transform.position);
                    }
                }
            }
        }
    }
}
