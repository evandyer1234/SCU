using Helpers;
using UI;
using UnityEngine;

public class MG_HeartString : MiniGameBase
{

    [SerializeField, Tooltip("A reference to the used icons on the top")] 
    private SpriteRenderer[] _icons;
    
    [SerializeField, Tooltip("The hints for which to display clicking hints")]
    private InteractionHint[] _veinOutlineHints;
    
    [SerializeField, Tooltip("A reference to the corruption icon")] 
    private Sprite _corruptionIcon;

    
    private int _correctVeinNumber;

    public override void Start()
    {
        base.Start();
        ingredient = NamingConstants.INGREDIENT_ID_NETTLE;
        
        _correctVeinNumber = Random.Range(0, _icons.Length);
        _icons[_correctVeinNumber].sprite = _corruptionIcon;
        
        Invoke(nameof(AnimateVeinClickAreas), 2f);
    }

    private void AnimateVeinClickAreas()
    {
        foreach (var _veinHint in _veinOutlineHints)
        {
            _veinHint.TriggerAnimation();
        }
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
