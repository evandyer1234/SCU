using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager instance;
    
    [SerializeField] private List<MiniGameBase> miniGames;

    [SerializeField] private MiniGameBase activeMiniGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetActiveMiniGame(miniGames[0]);
    }
    
    public void SetActiveMiniGame(MiniGameBase _miniGame)
    {
        activeMiniGame = _miniGame;
        SwapToNewMiniGame();
    }

    public void SwapToNewMiniGame()
    {
        SetInstructionTextToCurrentMiniGame();    
    }
    


    public void SetInstructionTextToCurrentMiniGame()
    {
        UIManager.instance.SetInstructionText("Instructions: " + "\n" + "\n" + activeMiniGame.miniGameInstructions);
    }
    
}
