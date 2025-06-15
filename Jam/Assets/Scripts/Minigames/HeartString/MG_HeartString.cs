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

    private PauseMenuManager _pauseMenuManager;
    private int _correctVeinNumber;

    public override void Start()
    {
        _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
            .GetComponent<PauseMenuManager>();
        
        base.Start();
        
        _correctVeinNumber = Random.Range(0, _icons.Length);
        _icons[_correctVeinNumber].sprite = _corruptionIcon;
        
        var randomPattern = _heartStringPatterns[Random.Range(0, _heartStringPatterns.Count)];
        randomPattern.gameObject.SetActive(true);
    }

    public void Selection(int index)
    {
        if (_pauseMenuManager.isGamePaused()) return;
        
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
