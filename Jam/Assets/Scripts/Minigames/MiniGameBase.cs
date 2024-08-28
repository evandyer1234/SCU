using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameBase : MonoBehaviour
{
    public GameObject piece;

    public string miniGameInstructions;
    
    [SerializeField] internal Stage miniGameStage;
    
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
        piece.SetActive(true);
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
