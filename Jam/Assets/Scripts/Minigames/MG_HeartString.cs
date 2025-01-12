using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MG_HeartString : MiniGameBase
{
    [SerializeField] List<HS_SO_Base> HSIMAGES = new List<HS_SO_Base>();
    int imageindex;
    [SerializeField] SpriteRenderer PuzzleImage;
    
   
    public override void Start()
    {
        base.Start();
        imageindex = Random.Range(0, HSIMAGES.Count);
        PuzzleImage.sprite = HSIMAGES[imageindex].Puzzleim;
    }

    
    public void Selection(int index)
    {
        if (index == HSIMAGES[imageindex].CorrectNum)
        {
            OnSuccess();
        }
        else
        {
            gameModeManager.subtractTime(penalty);

        }
    }
}
