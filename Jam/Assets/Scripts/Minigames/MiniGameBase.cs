using Helpers;
using TMPro;
using UnityEngine;

public class MiniGameBase : MonoBehaviour
{
    public float penalty;

    [SerializeField] internal Stage miniGameStage;
    protected MiniGameManager miniGameManager;
    private TextMeshProUGUI debugMessage;
    private GameObject corruptionMarkRef;

    public virtual void Start()
    {
        miniGameManager = GameObject.FindObjectOfType<MiniGameManager>();
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

    public bool IsMinigameRunning()
    {
        return gameObject.activeInHierarchy;
    }
    
    public void OnSuccess()
    {
        debugMessage.text = "Success";
        gameObject.SetActive(false);

        if (corruptionMarkRef != null)
        {
            corruptionMarkRef.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No corruptionmark found");
        }

        if (miniGameManager != null)
        {
            miniGameManager.FinishMiniGame(this);
        }
        else
        {
            Debug.LogWarning("No minigamemananger found");
        }
    }

    public void OnPenalty()
    {
        debugMessage.text = "Failure";
        if (miniGameManager == null)
        {
            miniGameManager.subtractTime(penalty);
        }
        else
        {
            Debug.LogWarning("No minigamemananger found");
        }
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
