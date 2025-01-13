using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
/*
    public List<string> scenes = new List<string>();


    public int currentSceneIndex = 0;

    private static SceneLoader _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextScene()
    {
        if (currentSceneIndex < scenes.Count - 1)
        {
            currentSceneIndex++;
            SceneManager.LoadScene(scenes[currentSceneIndex]);
        }
        else
        {
            Debug.Log("All scenes completed!");
        }
    }

    public void RestartSceneList()
    {
        currentSceneIndex = 0;
        if (scenes.Count > 0)
        {
            SceneManager.LoadScene(scenes[currentSceneIndex]);
        }
    }

    public void LoadSceneByName(string sceneName)
    {
        if (scenes.Contains(sceneName))
        {
            currentSceneIndex = scenes.IndexOf(sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene not found in the list: " + sceneName);
        }
    }

    public bool HasMoreScenes()
    {
        return currentSceneIndex < scenes.Count - 1;
    }*/
}
