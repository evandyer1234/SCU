using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public float sandFallThreshold;

    private float startTime;
    private float currentTime;

    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private TMP_Text timerText;

    [SerializeField] private Image hourglassSandTop, hourglassSandMid, hourglassSandBottom;



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

    private void Start()
    {
        startTime = GameModeManager.instance.CurrentTime;
    }

    public void Update()
    {
        currentTime = GameModeManager.instance.CurrentTime;
        
        UpdateHourGlassSandImageFill();
    }

    private void UpdateHourGlassSandImageFill()
    {
        // get percentage of time passed
        float timePassed = currentTime / startTime;
        float timeRemaining = 1 - timePassed;

        // set image fill percentage to be that of the above percentage
        hourglassSandTop.fillAmount = timePassed;
        hourglassSandBottom.fillAmount = timeRemaining;
        
        // handle falling sand fill percentage
        if (timeRemaining >= sandFallThreshold)
        {
            float adjustedTimeRemaining = (1 - timeRemaining) / (1 - sandFallThreshold);
            hourglassSandMid.fillAmount = adjustedTimeRemaining;
            Debug.Log(adjustedTimeRemaining);
        }
        else
        {
            hourglassSandMid.fillAmount = 1f;
        }
    }
}
