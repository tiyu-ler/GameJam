/*using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour {
    public GameObject cnvMain;     
    public GameObject cnvSettings; 
    public TMP_InputField timer1;
    public TMP_InputField score1;
    public TMP_InputField timer2;
    public TMP_InputField score2;
    public TMP_InputField timer3;
    public TMP_InputField score3;

    public void StartGame() {
        SceneManager.LoadScene("level1");
    }

    public void QuitGame() {
        Application.Quit(); 
        Debug.Log("Game Quit");
    }

    public void ShowSettings() { 
        cnvMain.SetActive(false);
        cnvSettings.SetActive(true);
        LoadSettings();  
    }

    public void ShowMain() {
        cnvMain.SetActive(true);
        cnvSettings.SetActive(false);
    }

    public void SaveAndCloseSettings() {
        float music = GameObject.Find("slMusic").GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("music", music);
        float effects = GameObject.Find("slEffect").GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("effect", effects);
        float spawner = GameObject.Find("slGenSpeed").GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("spawner", spawner);

        SaveTimerAndScore(timer1, score1, "time1", "score1");
        SaveTimerAndScore(timer2, score2, "time2", "score2");
        SaveTimerAndScore(timer3, score3, "time3", "score3");

        ShowMain();
    }

    private void SaveTimerAndScore(TMP_InputField timerInput, TMP_InputField scoreInput, string timerKey, string scoreKey) {
        if (float.TryParse(timerInput.text, out float timerValue)) {
            PlayerPrefs.SetFloat(timerKey, timerValue);
        } else {
            Debug.LogWarning($"Invalid timer value for {timerKey}: {timerInput.text}");
        }

        if (float.TryParse(scoreInput.text, out float scoreValue)) {
            PlayerPrefs.SetFloat(scoreKey, scoreValue);
        } else {
            Debug.LogWarning($"Invalid score value for {scoreKey}: {scoreInput.text}");
        }
    }

    private void LoadSettings() {
        if (PlayerPrefs.HasKey("time1")) {
            timer1.text = PlayerPrefs.GetFloat("time1").ToString();
        }
        if (PlayerPrefs.HasKey("score1")) {
            score1.text = PlayerPrefs.GetFloat("score1").ToString();
        }
        if (PlayerPrefs.HasKey("time2")) {
            timer2.text = PlayerPrefs.GetFloat("time2").ToString();
        }
        if (PlayerPrefs.HasKey("score2")) {
            score2.text = PlayerPrefs.GetFloat("score2").ToString();
        }
        if (PlayerPrefs.HasKey("time3")) {
            timer3.text = PlayerPrefs.GetFloat("time3").ToString();
        }
        if (PlayerPrefs.HasKey("score3")) {
            score3.text = PlayerPrefs.GetFloat("score3").ToString();
        }
    }
}*/