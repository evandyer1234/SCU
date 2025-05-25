using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Subjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    Dictionary<string, bool> miniGamesFinishedState = new();
    private List<string> collectedIngredientsPerPatient = new();
    private float CurrentTime;
    private SubjectManager _subjectManager;
    
    private void Awake()
    {
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
        CurrentTime = StartTime;
        Time.timeScale = 1f;
    }
    
    void FixedUpdate()
    {
        CurrentTime -= Time.fixedDeltaTime;
        SetTimerText("" + TimeSpan.FromSeconds(CurrentTime).Minutes.ToString("00") + " : " + TimeSpan.FromSeconds(CurrentTime).Seconds.ToString("00"));
        if (CurrentTime <= 0)
        {
            Time.timeScale = 0;
        }
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
                AddIngredient(minigameBase.ingredient);
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
    
    public void AddIngredient(string ingredientName)
    {
        collectedIngredientsPerPatient.Add(ingredientName);
        _subjectManager.AddUniqueIngredient(ingredientName);

        string ingredientsToDisplay = "Ingredients:\n";
        foreach (var ingName in collectedIngredientsPerPatient)
        {
            ingredientsToDisplay += "\n* " + ingName;
        }
        postItIngredients.GetComponent<TextMeshPro>().text = ingredientsToDisplay;
    }

    public void subtractTime(float amount)
    {
        CurrentTime -= amount;
    }
    
    private bool AllMinigamesFinished()
    {
        return miniGamesFinishedState.Where(state => state.Value)
            .ToList().Count() == miniGames.Count;
    }
    
    private void SetTimerText(string _inText)
    {
        timerText.text = _inText;
    }

    public void ReloadCurrentScene()
    {
        var _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
            .GetComponent<SubjectManager>();
        var subjectNameBefore = _subjectManager.currentSubject.name;
        _subjectManager.ResetMinigameState();
        _subjectManager.LaunchMinigames(subjectNameBefore);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToPotionMakingScene()
    {
        if (!AllMinigamesFinished()) return;
        
        SceneManager.LoadScene(NamingConstants.SCENE_MAIN_ALCHEMY);
    }
    
    public void Win()
    {
        Time.timeScale = 0;
    }
}
