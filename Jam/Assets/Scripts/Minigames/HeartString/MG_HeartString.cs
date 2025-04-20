using TMPro;
using UnityEngine;

public class MG_HeartString : MiniGameBase
{

    [SerializeField, Tooltip("A reference to the used icons on the top")] 
    private SpriteRenderer[] _icons;
    
    [SerializeField, Tooltip("A reference to the corruption icon")] 
    private Sprite _corruptionIcon;
    
    [SerializeField, Tooltip("A reference to temporary display feedback for the Minigame")] 
    private TextMeshProUGUI debugMessage;
    
    private int _correctVeinNumber;

    void Awake()
    {
        debugMessage.text = "";
    }
    
    public override void Start()
    {
        base.Start();
        
        _correctVeinNumber = Random.Range(0, _icons.Length);

        _icons[_correctVeinNumber].sprite = _corruptionIcon;
    }

    public void Selection(int index)
    {
        if (index == _correctVeinNumber)
        {
            OnSuccess();
            DisplayFeedback("Success!");
        }
        else
        {
            gameModeManager.subtractTime(penalty);
            DisplayFeedback("Failure...");
        }
    }

    private void DisplayFeedback(string text)
    {
        debugMessage.text = text;
    }
}
