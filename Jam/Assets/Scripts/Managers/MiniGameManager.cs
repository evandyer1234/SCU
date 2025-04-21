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
}
