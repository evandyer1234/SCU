using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _paused;

    private SCUInputAction _scuInputAction;
    private List<HoverTooltip> _tooltipsInScene;
    private SubjectManager _subjectManager;

    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
        _tooltipsInScene = GetAllTooltipsInScene();

        Resume();
    }

    private void Start()
    {
        _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM).GetComponent<SubjectManager>();
    }
    
    private void Update()
    {
        if (_subjectManager.GetSCUSceneManager().IsMainMenuScene()) return;
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

        EnableAllTooltipsInScene(true);
    }

    public bool isGamePaused()
    {
        return _paused;
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
            if (tooltip != null)
            {
                tooltip.ForceDisable();
                tooltip.gameObject.SetActive(b);
            }
        }
    }

    List<HoverTooltip> GetAllTooltipsInScene()
    {
        List<HoverTooltip> tooltipsList = new List<HoverTooltip>();

        foreach (HoverTooltip tooltip in Resources.FindObjectsOfTypeAll(typeof(HoverTooltip)) as HoverTooltip[])
        {
            if (!(tooltip.hideFlags == HideFlags.NotEditable || tooltip.hideFlags == HideFlags.HideAndDontSave))
            {
                tooltipsList.Add(tooltip);
            }
        }

        return tooltipsList;
    }
}
