using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Alchemy : MiniGameBase
{
    public Button startButton;
    public Button nextMinigameButton;

    [SerializeField] private List<MG_AlchemyBase> minigames;
    // Index for currently active minigame
    private int activeMinigame = 0;
    
    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        Button btn2 = nextMinigameButton.GetComponent<Button>();
        btn.onClick.AddListener(StartButtonClicked);
        btn2.onClick.AddListener(NextMinigameButtonClicked);
    }

    
    void Update()
    {
        if (activeMinigame < minigames.Count)
        {
            nextMinigameButton.GetComponent<Button>().interactable = minigames[activeMinigame].gameFinished;
        }
    }

    void StartButtonClicked()
    {
        Debug.Log(minigames[activeMinigame].gameObject.activeInHierarchy);
        minigames[activeMinigame].gameObject.SetActive(true);
    }

    void NextMinigameButtonClicked()
    {
        minigames[activeMinigame].gameObject.SetActive(false);
        activeMinigame++;
        minigames[activeMinigame].gameObject.SetActive(true);
    }
}
