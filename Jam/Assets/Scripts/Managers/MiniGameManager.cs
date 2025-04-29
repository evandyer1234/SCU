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
    [SerializeField] private List<MiniGameBase> miniGames;
    [SerializeField] private MiniGameBase activeMiniGame;
    [SerializeField] float StartTime;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] GameObject GameoverPanel;
    [SerializeField] GameObject GamewinPanel;
    [SerializeField, Tooltip("A reference to the magnifying Glass Shadow freezing/unfreezing usage")]
    private GameObject magnifyingGlassShadowRef;
    [SerializeField, Tooltip("A reference to the post it ingredients displaying progress")] 
    GameObject postItIngredients;
    
    Dictionary<string, bool> miniGamesFinishedState = new();
    private List<string> collectedIngredientNames = new();
    private float CurrentTime;
    private SCUInputAction _scuInputAction;
    
    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
        
        foreach (var miniGame in miniGames)
        {
            miniGame.Disable();
            miniGamesFinishedState.Add(miniGame.name, false);
        }
    }
    
    void Start()
    {
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
            GameoverPanel.SetActive(true);
        }
        
        if (KeyboardInput.EscapePressed(_scuInputAction)) {
            SceneManager.LoadScene(NamingConstants.SCENE_ID_MAIN_MENU);
        }
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
            Win();
        }
    }
    
    public void AddIngredient(string ingredientName)
    {
        collectedIngredientNames.Add(ingredientName);

        string ingredientsToDisplay = "Ingredients:\n";
        foreach (var ingName in collectedIngredientNames)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Win()
    {
        Time.timeScale = 0;
        GamewinPanel.SetActive(true);
    }
}
