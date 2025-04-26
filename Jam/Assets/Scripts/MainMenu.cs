using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartLevel(string levelname)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(levelname);
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
