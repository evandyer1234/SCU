
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
   
    public void StartLevel(string  levelname)
    {
        SceneManager.LoadScene(levelname);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void enterlevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void exitgame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Scene0()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
}
