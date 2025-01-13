using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class constellationController : MonoBehaviour
{
    public int SpotCount;
    public int MaxSpotCount;
    public TMP_Text scoreText;
    public GameObject GameCompletedMenu;
    // private MenuScript menuhandler;
    public string NextSceneName;
    public stateHandler stateHandler;
    private bool isFinished = false;


    private void Start()
    {
        //menuhandler = GameObject.Find("InGameMenuHandler").GetComponent<MenuScript>();
        isFinished = false;
        if (SoundManager.sndm != null)
        {
            SoundManager.sndm.StopAllSounds();
            SoundManager.sndm.Play("SpaceTheme");
        }
    }

    void Update()
    {

        if (SpotCount == MaxSpotCount)
        {
            //   menuhandler.TriggerPause();
            scoreText.text = "";
            GameCompletedMenu.SetActive(true);
            stateHandler.isCompleted = true;
            if (SoundManager.sndm != null&&isFinished==false)
            {
                SoundManager.sndm.Play("Fanfare");
            }
            isFinished=true;
            Invoke("LoadNextLevel", 3.5f);
        }
        else
        {
            scoreText.text = SpotCount + "/" + MaxSpotCount;
        }
    }
    void LoadNextLevel()
    {
        SoundManager.sndm.StopAllSounds();
        SceneManager.LoadScene(NextSceneName);
    }
}
