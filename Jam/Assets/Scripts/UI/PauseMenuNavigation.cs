using Helpers;
using UnityEngine;

//add UI namespace?
public class PauseMenuNavigation : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        
        Time.timeScale = 1;

        var subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
            .GetComponent<SubjectManager>();
        subjectManager.ResetMinigameState();
        subjectManager.GetSCUSceneManager().TransitionToScene(NamingConstants.SCENE_ID_MAIN_MENU);
        var pauseMenuManager = FindObjectOfType<PauseMenuManager>();
        pauseMenuManager.CleanupGameObjects();
        pauseMenuManager.Resume();
    }
}

