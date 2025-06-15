using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Subjects;
using TMPro;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] public List<MiniGameBase> miniGames;
    [SerializeField] float StartTime;
    [SerializeField] private TMP_Text timerText;
    [SerializeField, Tooltip("A reference to the magnifying Glass Shadow freezing/unfreezing usage")]
    private GameObject magnifyingGlassShadowRef;
    [SerializeField, Tooltip("A reference to the post it ingredients displaying progress")] 
    GameObject postItIngredients;
    [SerializeField] private GameObject goToPotionSceneButton;
    [SerializeField] private GameObject minigameDoneText;
    [SerializeField] private GameObject clothLayerPatient;
    
    Dictionary<string, bool> miniGamesFinishedState = new();
    private List<string> collectedIngredientsPerPatient = new();
    private float CurrentTime;
    private SubjectManager _subjectManager;
    private PauseMenuManager _pauseMenuManager;
    
    private void Awake()
    {
        _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
            .GetComponent<PauseMenuManager>();
        
        foreach (var miniGame in miniGames)
        {
            miniGame.Disable();
            miniGamesFinishedState.Add(miniGame.name, false);
        }
        
        goToPotionSceneButton.SetActive(false);
        minigameDoneText.SetActive(false);
        

    }
    
    void Start()
    {
        _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
            .GetComponent<SubjectManager>();
        collectedIngredientsPerPatient = new();
        InitializeTimer();

        if (_subjectManager.IsPotionMode())
        {
            clothLayerPatient.GetComponent<Collider2D>().enabled = true;
            postItIngredients.SetActive(false);
        }
        else
        {
            clothLayerPatient.GetComponent<Collider2D>().enabled = false;
        }
    }
    
    void FixedUpdate()
    {
        if (_subjectManager.IsPotionMode())
        {
            SetTimerText("");
            return;
        }
        
        CurrentTime -= Time.fixedDeltaTime;
        UpdateTimerText();
        if (CurrentTime <= 0 && !_pauseMenuManager.isGamePaused())
        {
            _pauseMenuManager.SetGameOver();
            _pauseMenuManager.Pause();
            CurrentTime = 0;
        }
    }

    private void UpdateTimerText()
    {
        SetTimerText("" + TimeSpan.FromSeconds(CurrentTime).Minutes.ToString("00") + " : " + TimeSpan.FromSeconds(CurrentTime).Seconds.ToString("00"));
    }

    public bool IsAnyMinigameRunning()
    {
        foreach (var miniGameBase in miniGames)
        {
            if (miniGameBase.IsMinigameRunning()) return true;
        }
        return false;
    }
    
    public void FinishMiniGame(MiniGameBase minigameBase)
    {
        string miniGameName = minigameBase.gameObject.name;
        
        foreach (var miniGameState in miniGamesFinishedState)
        {
            if (miniGameState.Key == miniGameName)
            {
                miniGamesFinishedState[miniGameName] = true;
                RevealIngredientHintsBasedOnFinishedMinigames();
                magnifyingGlassShadowRef.GetComponent<MagnifyingGlassShadow>().SetMagnifyingGlassInUse(true);
                break;
            }
        }

        if (AllMinigamesFinished())
        {
            goToPotionSceneButton.SetActive(true);
            minigameDoneText.SetActive(true);
        }
    }
    
    public void RevealIngredientHintsBasedOnFinishedMinigames()
    {
        var finishedMinigamesCount = miniGamesFinishedState.Where(state => state.Value)
            .ToList().Count();
        var totalIngrHints = _subjectManager.currentSubject.ingredientHints;

        string ingredientsToDisplay = "Ingredient Hints:\n";
        for (int i = 0; i < finishedMinigamesCount; i++)
        {
            ingredientsToDisplay += "\n* " + totalIngrHints[i];
        }
        postItIngredients.GetComponent<TextMeshPro>().text = ingredientsToDisplay;
    }

    public void subtractTime(float amount)
    {
        CurrentTime -= amount;
        if (CurrentTime <= 0)
        {
            CurrentTime = 0;
            _pauseMenuManager.SetGameOver();
            _pauseMenuManager.Pause();
        }
        UpdateTimerText();
    }

    public void InitializeTimer()
    {
        CurrentTime = StartTime;
        Time.timeScale = 1f;
    }
    
    private bool AllMinigamesFinished()
    {
        return miniGamesFinishedState.Where(state => state.Value)
            .ToList().Count() == _subjectManager.currentSubject.subjectMinigames.Count();
    }
    
    private void SetTimerText(string _inText)
    {
        timerText.text = _inText;
    }

    public void GoToPotionMakingScene()
    {
        if (!AllMinigamesFinished()) return;
        _subjectManager.GetSCUSceneManager().TransitionToScene(NamingConstants.SCENE_MAIN_ALCHEMY);
    }
    
    public void Win()
    {
        Time.timeScale = 0;
    }
}
