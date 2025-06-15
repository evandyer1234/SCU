using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _gameOverMinigamePanel;
    [SerializeField] private GameObject _gameOverPotionPanel;

    private bool _paused;
    private bool _gameOver;

    private SCUInputAction _scuInputAction;
    private List<HoverTooltip> _tooltipsInScene;
    private List<HoverOutline> _outlinesInScene;
    private SubjectManager _subjectManager;

    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
        _tooltipsInScene = GetAllTooltipsInScene();
        _outlinesInScene = GetAllOutlinesInScene();

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
        //AkSoundEngine.SetRTPCValue("isPaused", 0, null);
        Time.timeScale = 1f;

        EnableAllTooltipsInScene(true);
        EnableAllOutlinesInScene(true);
    }

    public void Restart()
    {
        Resume();
        ReloadCurrentScene();
    }

    public bool isGamePaused()
    {
        return _paused;
    }

    public void SetGameOver()
    {
        _gameOver = true;
        Pause();
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        _paused = true;
        //AkSoundEngine.SetRTPCValue("isPaused", 1, null);
        Time.timeScale = 0f;
        if (_gameOver)
        {
            _resumeButton.SetActive(false);
            _restartButton.SetActive(true);
            if (_subjectManager.IsPotionMode())
            {
                _gameOverPotionPanel.SetActive(true);
            }
            else
            {
                _gameOverMinigamePanel.SetActive(true);
            }
        }
        else
        {
            _resumeButton.SetActive(true);
            _restartButton.SetActive(false);
            _gameOverMinigamePanel.SetActive(false);
            _gameOverPotionPanel.SetActive(false);
        }

        EnableAllTooltipsInScene(false);
        EnableAllOutlinesInScene(false);
    }

    public void ReloadCurrentScene()
    {
        var _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
            .GetComponent<SubjectManager>();
        _subjectManager.SetPotionMode(false);
        var subjectNameBefore = _subjectManager.currentSubject.name;
        _subjectManager.ResetMinigameState();
        _subjectManager.GetSCUSceneManager().TransitionToScene(SceneManager.GetActiveScene().name, true);
        _subjectManager.LaunchMinigames(subjectNameBefore);
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


    void EnableAllOutlinesInScene(bool b)
    {
        foreach (HoverOutline outline in _outlinesInScene)
        {
            if (outline != null)
            {
                outline.gameObject.SetActive(b);
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


    //this method does pretty much the same thing as the previous one
    //it would be nice to use a single method with variable type instead
    //however using variable types looks pretty complicated and not worth the time for a minor issue like this imo
    List<HoverOutline> GetAllOutlinesInScene()
    {
        List<HoverOutline> outlineList = new List<HoverOutline>();

        foreach (HoverOutline outline in Resources.FindObjectsOfTypeAll(typeof(HoverOutline)) as HoverOutline[])
        {
            if (!(outline.hideFlags == HideFlags.NotEditable || outline.hideFlags == HideFlags.HideAndDontSave))
            {
                outlineList.Add(outline);
            }
        }

        return outlineList;
    }
}
