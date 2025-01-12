using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniGameBase : MonoBehaviour
{
    public GameObject piece;

    public string miniGameInstructions;
    public float penalty;

    [SerializeField] internal Stage miniGameStage;
    [HideInInspector] public GameModeManager gameModeManager;
    public virtual void Start()
    {
        gameModeManager = EventSystem.current.gameObject.GetComponent<GameModeManager>();
    }
    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Update()
    {
        
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnSuccess()
    {
        if (piece != null)
        {
            piece.SetActive(true);
        }
        
        gameObject.SetActive(false);
    }
    
    public virtual void SwapStage(int num)
    {
        switch (num)
        {
            case 1:
                miniGameStage = Stage.Stage01;
                break;
            case 2:
                miniGameStage = Stage.Stage02;
                break;
            case 3:
                miniGameStage = Stage.Stage03;
                break;
            default:
                break;
        }
    }
}

enum Stage {
    Stage01,
    Stage02,
    Stage03
}
