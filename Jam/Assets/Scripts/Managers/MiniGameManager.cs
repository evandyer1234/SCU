using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private List<MiniGameBase> miniGames;

    [SerializeField] private MiniGameBase activeMiniGame;

    private void Awake()
    {
        foreach (var miniGame in miniGames)
        {
            miniGame.Disable();
        }
    }
    
    public void ActivateMinigameByTag(string minigameTag)
    {
        foreach (var miniGame in miniGames)
        {
            if (miniGame.gameObject.CompareTag(minigameTag))
            {
                miniGame.Enable();
                activeMiniGame = miniGame;
            }
        }
    }
    
    public void SetInstructionTextToCurrentMiniGame()
    {
        UIManager.instance.SetInstructionText("Instructions: " + "\n" + "\n" + activeMiniGame.miniGameInstructions);
    }
    
}
