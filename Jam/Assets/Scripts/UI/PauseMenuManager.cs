using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _gameOverPanel;

    private bool _paused;
    private bool _gameOver;

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
        //AkSoundEngine.SetRTPCValue("isPaused", 0, null);
        Time.timeScale = 1;

        EnableAllTooltipsInScene(true);
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
        Time.timeScale = 0;

        if (_gameOver)
        {
            _resumeButton.SetActive(false);
            _restartButton.SetActive(true);
            _gameOverPanel.SetActive(true);
        }
        else
        {
            _resumeButton.SetActive(true);
            _restartButton.SetActive(false);
            _gameOverPanel.SetActive(false);
        }
        
        EnableAllTooltipsInScene(false);
    }
    
    public void ReloadCurrentScene()
    {
        var _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
            .GetComponent<SubjectManager>();
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
