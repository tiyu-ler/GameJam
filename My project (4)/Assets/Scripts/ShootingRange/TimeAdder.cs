using UnityEngine;

public class TimeAdder : MonoBehaviour
{
    public int timeAmount = 10;
    public ParticleSystem ps;
      private void OnCollisionEnter(Collision collision)
      {
          if (collision.gameObject.CompareTag("Projectile"))
          {
              if (GameManager.gm != null)
              {
                  GameManager.gm.ChangeScoreTime(0, timeAmount);
              }
              Instantiate(ps, transform.position, Quaternion.Euler(-90,0,0));
              Destroy(collision.gameObject);
              Destroy( gameObject);
          }
      }/*
      private void OnTriggerEnter(Collider collision){
          if (collision.gameObject.CompareTag("Projectile"))
          {
              if (GameManager.gm != null)
              {
                  GameManager.gm.ChangeScoreTime(0, timeAmount);
              }
              Instantiate(ps, transform.position, Quaternion.Euler(-90,0,0));
              Destroy(collision.gameObject);
              Destroy( gameObject);
          }
      }*/
/*    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (GameManager.gm != null)
            {
                GameManager.gm.ChangeScoreTime(0, timeAmount);
            }
            Instantiate(ps, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }*/
}
