using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject PauseMenu, SceneUi;
    public stateHandler stateHandler;

    void Start()
    {
        stateHandler.isPaused = false;
        stateHandler.isCompleted = false;

        PauseMenu.SetActive(false);
        SceneUi.SetActive(true);
        //  GameOverMenu.SetActive(false);
        //  LevelPassedMenu.SetActive(false);
    }

    void Update()
    {
        if (!stateHandler.isCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TriggerPause();
            }
        }
    }

    void gamepause()
    {
        SceneUi.SetActive(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void gameOver()
    {
        // GameOverMenu.SetActive(true);
        Time.timeScale = 0;
        stateHandler.isCompleted = true;
    }

    public void gamecontinue()
    {
        stateHandler.isPaused = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneUi.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;

        Debug.Log("pog");
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
        stateHandler.isPaused = !stateHandler.isPaused;

        if (stateHandler.isPaused)
        {
            gamepause();
        }
        else
        {
            gamecontinue();
        }
    }
}
