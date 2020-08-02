using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        //Application.LoadLevel(sceneName);
        //
    }
    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
