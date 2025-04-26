using System.Collections.Generic;
using System.Linq;
using Subjects;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private List<MiniGameBase> miniGames;

    [SerializeField] private MiniGameBase activeMiniGame;
    
    [SerializeField, Tooltip("A reference to the magnifying Glass Shadow freezing/unfreezing usage")] 
    private GameObject magnifyingGlassShadowRef;

    private GameModeManager gameModeManager;
    
    Dictionary<string, bool> miniGamesFinishedState = new();
    
    private void Awake()
    {
        gameModeManager = FindObjectOfType<GameModeManager>();
        
        foreach (var miniGame in miniGames)
        {
            miniGame.Disable();
            miniGamesFinishedState.Add(miniGame.name, false);
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
                gameModeManager.AddIngredient(minigameBase.ingredient);
                magnifyingGlassShadowRef.GetComponent<MagnifyingGlassShadow>().SetMagnifyingGlassInUse(true);
                break;
            }
        }

        if (AllMinigamesFinished())
        {
            gameModeManager.Win();
        }
    }

    private bool AllMinigamesFinished()
    {
        return miniGamesFinishedState.Where(state => state.Value)
            .ToList().Count() == miniGames.Count;
    }
}
