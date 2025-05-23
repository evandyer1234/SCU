using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;


public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _paused;

    private SCUInputAction _scuInputAction;
    private EventSystem _eventSystem;

    private List<HoverTooltip> _tooltipsInScene;


    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();

        _eventSystem = FindObjectOfType<EventSystem>();

        _tooltipsInScene = GetAllTooltipsInScene();

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

        DisableAllTooltipsInScene();
    }

    void DisableAllTooltipsInScene()
    {
        /*
        foreach (HoverTooltip tooltip in Resources.FindObjectsOfTypeAll(typeof(HoverTooltip)) as HoverTooltip[])
        {
            if (!EditorUtility.IsPersistent(tooltip.transform.root.gameObject) && !(tooltip.hideFlags == HideFlags.NotEditable || tooltip.hideFlags == HideFlags.HideAndDontSave))
            {
                tooltip.ForceDisable();
            }
        }
        */

        foreach (HoverTooltip tooltip in _tooltipsInScene)
        {
            tooltip.ForceDisable();
        }
    }

    List<HoverTooltip> GetAllTooltipsInScene()
    {
        List<HoverTooltip> tooltipsList = new List<HoverTooltip>();

        foreach (HoverTooltip tooltip in Resources.FindObjectsOfTypeAll(typeof(HoverTooltip)) as HoverTooltip[])
        {
            if (!EditorUtility.IsPersistent(tooltip.transform.root.gameObject) && !(tooltip.hideFlags == HideFlags.NotEditable || tooltip.hideFlags == HideFlags.HideAndDontSave))
            {
                tooltipsList.Add(tooltip);
            }
        }

        return tooltipsList;
    }

}
