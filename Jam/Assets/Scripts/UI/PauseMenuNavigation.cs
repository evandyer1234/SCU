using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

//add UI namespace?
public class PauseMenuNavigation : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        var _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
            .GetComponent<SubjectManager>();
        _subjectManager.ResetMinigameState();
        SceneManager.LoadScene(NamingConstants.SCENE_ID_MAIN_MENU);
    }
}

