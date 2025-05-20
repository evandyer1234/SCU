using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;


public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _paused;

    private SCUInputAction _scuInputAction;
    private EventSystem _eventSystem;

    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();

        _eventSystem = FindObjectOfType<EventSystem>();

        Resume();
    }
    
    private void Update()
    {
        if (KeyboardInput.EscapePressed(_scuInputAction))
        {
            if(_paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        _paused = false;
        Time.timeScale = 1;

        _eventSystem.SetSelectedGameObject(null);
    }

    private void Pause()
    {
        _pauseMenu.SetActive(true);
        _paused = true;
        Time.timeScale = 0;
    }

}
