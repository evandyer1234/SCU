using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private TMP_Text timerText;


    public void SetInstructionText(string _inText)
    {
        instructionText.text = _inText;
    }

    public void SetTimerText(string _inText)
    {
        timerText.text = _inText;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
