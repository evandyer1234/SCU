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

    public void StartLevelProfile04()
    {
        StartLevel(SubjectManager.SUBJECT_NAME_04);
    }
    
    public void StartLevelProfile14()
    {
        StartLevel(SubjectManager.SUBJECT_NAME_14);
    }
    
    public void StartLevelProfile15()
    {
        StartLevel(SubjectManager.SUBJECT_NAME_15);
    }
    
    private void StartLevel(string subjectName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(NamingConstants.SCENE_MAIN_MINIGAME);
        _subjectManager.LaunchMinigames(subjectName);
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
