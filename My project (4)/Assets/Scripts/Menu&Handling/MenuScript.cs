using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject PauseMenu, Player, GameOverMenu, LevelPassedMenu, HealthBar;
    private bool pause, completed;
    public int levelnumber;

    void Start()
    {
        pause = false;
        completed = false;
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        LevelPassedMenu.SetActive(false);
        // bestScore = PlayerPrefs.GetInt("BestScore", 0);
        //PlayerPrefs.SetInt("BestScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            if (Input.GetKeyDown(KeyCode.Escape))// && Player.GetComponent<PlayerScript>().NotDead == true)
            {
                TriggerPause();
            }
            else //if (Player.GetComponent<PlayerScript>().NotDead == false)
            {
                gameOver();
            }
        }
    }
    void gamepause()
    {
        HealthBar.SetActive(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
     //   Player.GetComponent<PlayerScript>().isStopped = true;
    }
    void gameOver()
    {
        GameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void gamecontinue()
    {
        HealthBar.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
      //  Player.GetComponent<PlayerScript>().isStopped = false;
       // Player.GetComponent<PlayerScript>().NotDead = true;
    }
    public void LoadMainMenu()
    {
        gamecontinue();
        SceneManager.LoadScene("MainMenu");

    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    public void RestartGame()
    {
        gamecontinue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void TriggerPause()
    {
        pause = !pause;
        switch (pause)
        {
            case true:
                gamepause();
                break;
            case false:
                gamecontinue();
                break;
        }
    }
    public void levelPassed()
    {
        Time.timeScale = 0;
        LevelPassedMenu.SetActive(true);
        PlayerPrefs.SetInt("LelvelsPassed", int.Parse(SceneManager.GetActiveScene().name.Substring(5)));
        completed=true;
    }
}
