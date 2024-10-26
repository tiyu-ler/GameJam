using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreAmount = 10;
    public int timeAmount = 10;
    public bool isTimeTarget = false;
    public bool decreaser = false;
    public ParticleSystem ps;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            KillTarget();
        }
    }

    public void KillTarget()
    {
        if (isTimeTarget)
        {
            // Adjust time
            int adjustedTime = decreaser ? -timeAmount : timeAmount;
            GameManager.gm.ChangeScoreTime(0, adjustedTime);
        }
        else
        {
            // Adjust score
            int adjustedScore = decreaser ? -scoreAmount : scoreAmount;
            GameManager.gm.ChangeScoreTime(adjustedScore, 0);
        }

       // Debug.Log("Target hit!");
        Instantiate(ps, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(gameObject);
    }
}
