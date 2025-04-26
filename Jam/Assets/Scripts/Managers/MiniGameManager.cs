using System.Collections.Generic;
using Subjects;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private List<MiniGameBase> miniGames;

    [SerializeField] private MiniGameBase activeMiniGame;
    
    [SerializeField, Tooltip("A reference to the magnifying Glass Shadow freezing/unfreezing usage")] 
    private GameObject magnifyingGlassShadowRef;

    Dictionary<string, bool> miniGamesFinishedState = new();
    
    private void Awake()
    {
        foreach (var miniGame in miniGames)
        {
            miniGame.Disable();
            miniGamesFinishedState.Add(miniGame.name, false);
        }
    }

    public void FinishMiniGameByName(string miniGameName)
    {
        foreach (var miniGameState in miniGamesFinishedState)
        {
            if (miniGameState.Key == miniGameName)
            {
                miniGamesFinishedState[miniGameName] = true;
                magnifyingGlassShadowRef.GetComponent<MagnifyingGlassShadow>().SetMagnifyingGlassInUse(true);
                break;
            }
        }
    }
}
