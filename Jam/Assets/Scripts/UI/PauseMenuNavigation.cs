using Helpers;
using UnityEngine;

//add UI namespace?
public class PauseMenuNavigation : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;

        var _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
            .GetComponent<SubjectManager>();
        _subjectManager.ResetMinigameState();
        _subjectManager.GetSCUSceneManager().TransitionToScene(NamingConstants.SCENE_ID_MAIN_MENU);
    }
}

