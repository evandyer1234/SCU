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

        EnableAllTooltipsInScene(true);
    }

    private void Pause()
    {
        _pauseMenu.SetActive(true);
        _paused = true;
        Time.timeScale = 0;

        EnableAllTooltipsInScene(false);
    }

    void EnableAllTooltipsInScene(bool b)
    {
        foreach (HoverTooltip tooltip in _tooltipsInScene)
        {
            tooltip.ForceDisable();

            tooltip.gameObject.SetActive(b);
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
