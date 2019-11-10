using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void SwitchScenes(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
 }
