using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float time;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text gameOverText;

    public int score = 0;
    private bool isGameOver = false; 
    public static GameManager gm = null; // Singleton instance

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }


        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 0;
            GameOver();
        }

        timeText.text = time.ToString("F2");
    }

    public void ChangeScoreTime(int sc, float t)
    {
        if (isGameOver) return;

        score += sc;
        time += t;

        scoreText.text = "Score: " + score.ToString();
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over";
        Invoke("reloadScene",2f);
    }
    void reloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}