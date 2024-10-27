using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        // LelvelsPassed = PlayerPrefs.GetInt("LelvelsPassed",1);
        MainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    public void ResetProgress()
    {
        PlayerPrefs.SetInt("LelvelsPassed", 0);
    }
}
