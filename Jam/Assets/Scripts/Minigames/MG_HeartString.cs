using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MG_HeartString : MiniGameBase
{
    [SerializeField] List<HS_SO_Base> HSIMAGES = new List<HS_SO_Base>();
    private int _correctVeinNumber;
    [SerializeField] SpriteRenderer PuzzleImage;

    [SerializeField] private SpriteRenderer[] _icons;
    [SerializeField] private Sprite corruptionIcon;
    //[SerializeField] private Sprite heartIcon;

    
   
    public override void Start()
    {
        base.Start();
        
        _correctVeinNumber = Random.Range(0, _icons.Length);

        _icons[_correctVeinNumber].sprite = corruptionIcon;

        //_imageindex = Random.Range(0, HSIMAGES.Count);
        //PuzzleImage.sprite = HSIMAGES[imageindex].Puzzleim;
    }

    
    public void Selection(int index)
    {
        Debug.Log("selection");

        if (index == _correctVeinNumber)
        {
            OnSuccess();
            Debug.Log("success");
        }
        else
        {
            gameModeManager.subtractTime(penalty);
            Debug.Log("failure");

        }
    }
}
