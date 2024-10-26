using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreAmount = 10;
    public bool decreaser = false;

      private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                int adjustedScore = decreaser ? -scoreAmount : scoreAmount;

                GameManager.gm.ChangeScoreTime(adjustedScore, 0);
                Debug.Log("hit");
                Destroy(collision.gameObject);
                Destroy(gameObject);

            }
        }/*
        private void OnTriggerEnter(Collider collision){
            if (collision.gameObject.CompareTag("Projectile"))
            {
                int adjustedScore = decreaser ? -scoreAmount : scoreAmount;

                GameManager.gm.ChangeScoreTime(adjustedScore, 0);
                Debug.Log("hit");
                Destroy(collision.gameObject);
                Destroy(gameObject);

            }
        }*//*
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            int adjustedScore = decreaser ? -scoreAmount : scoreAmount;

            GameManager.gm.ChangeScoreTime(adjustedScore, 0);
            Debug.Log("hit");
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }*/
}
