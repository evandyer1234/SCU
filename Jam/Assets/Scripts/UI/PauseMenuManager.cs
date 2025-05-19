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
    private GameObject _eventSystemGO;
    private EventSystem _eventSystem;

    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();

        //not sure how to reference the event system since it's not included in the scene as it is?
        //_eventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        _eventSystemGO = GameObject.Find("EventSystem");
        if(_eventSystemGO == null)
        {
            _eventSystemGO = GameObject.Find("EventSystem(Clone)");
        }
        _eventSystem = _eventSystemGO.GetComponent<UnityEngine.EventSystems.EventSystem>();
        //^ this sucks

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
