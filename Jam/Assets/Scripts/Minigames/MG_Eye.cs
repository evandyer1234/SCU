using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MG_Eye : MiniGameBase
{
    [SerializeField] private List<GameObject> selectableEyes;
    [SerializeField] private GameObject lEye, rEye;

    [SerializeField] internal Iris intactEye;



    [SerializeField] private List<Sprite> irisColors;

    public override void Enable()
    {
        base.Enable();
        
        RandomizeIrisColors();
    }

    private void Awake()
    {
        if (lEye.GetComponentInChildren<Iris>().isIntactEye)
        {
            intactEye = lEye.GetComponentInChildren<Iris>();
        }
        else
        {
            intactEye = rEye.GetComponentInChildren<Iris>();
        }
        
        RandomizeIrisColors();
    }

    public override void Update()
    {
        base.Update();

        if (miniGameStage == Stage.Stage01)
        {
            // DO STAGE 1 STUFF HERE
            // 1. REMOVE INFECTED EYE FROM EYE SOCKET
        } else if (miniGameStage == Stage.Stage02)
        {
            // DO STAGE 2 STUFF HERE
            // 1. MAKE UNINFECTED EYE MOVE AT RANDOM, FAST INTERVALS, TO RANDOM LOCATIONS
            // 2. CHECK IF SELECTED GLASS EYE IS CORRECT COLOR
            // 3. IF NOT, KNOCK DOWN TIME
            // 4. IF SO , COMPLETE MINIGAME
        }
    }

    public void RemoveEye()
    {
        SwapStage(2);
    }


    void RandomizeIrisColors()
    {
        for (int i = 0; i < selectableEyes.Count; i++)
        {
            selectableEyes[i].GetComponentInChildren<Iris>().gameObject.GetComponent<SpriteRenderer>().sprite = irisColors[Random.Range(0, irisColors.Count)];
        }
    }
}
