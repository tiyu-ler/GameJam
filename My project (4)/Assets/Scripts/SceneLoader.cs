using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
    // List of scene names to load in order
    public List<string> scenes = new List<string>();

    // Keep track of the current scene index
    public int currentSceneIndex = 0;

    // Make this a singleton if you want to persist between scenes
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

    // Load the next scene in the list
    public void LoadNextScene()
    {
        if (currentSceneIndex < scenes.Count - 1)
        {
            currentSceneIndex++;
            SceneManager.LoadScene(scenes[currentSceneIndex]);
        }
        else
        {
            // Optionally, restart from the first scene or show a completion message
            Debug.Log("All scenes completed!");
        }
    }

    // Restart the scene list from the beginning
    public void RestartSceneList()
    {
        currentSceneIndex = 0;
        if (scenes.Count > 0)
        {
            SceneManager.LoadScene(scenes[currentSceneIndex]);
        }
    }

    // Load a specific scene by name (useful for main menu or specific levels)
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

    // Check if there are more scenes to load
    public bool HasMoreScenes()
    {
        return currentSceneIndex < scenes.Count - 1;
    }
}
