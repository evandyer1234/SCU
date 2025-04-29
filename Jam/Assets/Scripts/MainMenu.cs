using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SubjectManager _subjectManager;
    
    private void Awake()
    {
        _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
            .GetComponent<SubjectManager>();
    }

    public void StartLevel(string levelname)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(levelname);
        _subjectManager.SetMinigamesLaunched(true);
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
        SceneManager.LoadScene(NamingConstants.SCENE_ID_MAIN_MENU);
        Time.timeScale = 1.0f;
    }
}
