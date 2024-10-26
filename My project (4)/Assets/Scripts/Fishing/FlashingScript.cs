using UnityEngine;

public class Flasher : MonoBehaviour
{
    public GameObject projectile;
    public float power = 10.0f;
    public AudioClip shootSFX;
    public float DestroyTime;
    public bool useRigidbody = false,calculateCamDistance=false ;

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (projectile)
            {
                GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward*(calculateCamDistance?1.2f:0), transform.rotation);
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