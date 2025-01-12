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
    private MenuScript menuhandler;
    public String NextSceneName;
    private void Start()
    {
        menuhandler = GameObject.Find("InGameMenuHandler").GetComponent<MenuScript>();
        if(SoundManager.sndm!=null){
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
            Invoke("LoadNextLevel", 2f);
        }
        else
        {
            scoreText.text = SpotCount + "/" + MaxSpotCount;
        }
    }
    void LoadNextLevel()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}
