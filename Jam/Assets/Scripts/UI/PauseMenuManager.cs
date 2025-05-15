using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;


public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private SCUInputAction _scuInputAction;

    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();

        _pauseMenu.SetActive(false);
    }
    
    private void Update()
    {
        if (KeyboardInput.EscapePressed(_scuInputAction))
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            //put in separate method
            //add time scale etc
        }
    }

}
