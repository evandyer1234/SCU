using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniGameBase : MonoBehaviour
{
    public GameObject piece;
    public float penalty;

    [SerializeField] internal Stage miniGameStage;
    [HideInInspector] public GameModeManager gameModeManager;
    private MiniGameManager miniGameManager;
    private TextMeshProUGUI debugMessage;
    private GameObject corruptionMarkRef;

    public virtual void Start()
    {
        gameModeManager = EventSystem.current.gameObject.GetComponent<GameModeManager>();
        miniGameManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINIGAME_MANAGER).GetComponent<MiniGameManager>();
        debugMessage = GameObject.FindGameObjectWithTag(NamingConstants.TAG_DEBUG_MESSAGE_USER_FEEDBACK).GetComponent<TextMeshProUGUI>();
        debugMessage.text = "";
    }

    public void StartMinigame(GameObject markRef)
    {
        corruptionMarkRef = markRef;
        gameObject.SetActive(true);
    }
    
    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnSuccess()
    {
        if (piece != null)
        {
            piece.SetActive(true);
        }
        
        debugMessage.text = "Success";
        gameObject.SetActive(false);
        corruptionMarkRef.SetActive(false);
        miniGameManager.FinishMiniGameByName(gameObject.name);
    }

    public void OnPenalty()
    {
        debugMessage.text = "Failure";
    }
    
    public void SwapStage(int num)
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
        }
    }
}

enum Stage {
    Stage01,
    Stage02,
    Stage03
}
