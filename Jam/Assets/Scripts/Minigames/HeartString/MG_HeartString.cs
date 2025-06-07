using System.Collections.Generic;
using Helpers;
using Minigames.HeartString;
using UnityEngine;

public class MG_HeartString : MiniGameBase
{

    [SerializeField, Tooltip("A reference to the used icons on the top")] 
    private SpriteRenderer[] _icons;
    
    [SerializeField] List<HeartStringPattern> _heartStringPatterns;
    
    [SerializeField, Tooltip("A reference to the corruption icon")] 
    private Sprite _corruptionIcon;

    
    private int _correctVeinNumber;

    public override void Start()
    {
        base.Start();
        ingredient = IngredientConstants.INGREDIENT_ID_NETTLE;
        
        _correctVeinNumber = Random.Range(0, _icons.Length);
        _icons[_correctVeinNumber].sprite = _corruptionIcon;
        
        var randomPattern = _heartStringPatterns[Random.Range(0, _heartStringPatterns.Count)];
        randomPattern.gameObject.SetActive(true);
    }

    public void Selection(int index)
    {
        if (index == _correctVeinNumber)
        {
            OnSuccess();
        }
        else
        {
            miniGameManager.subtractTime(penalty);
            OnPenalty();
        }
    }
}
