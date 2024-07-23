using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] float StartTime;
    float CurrentTime;
    [SerializeField] TextMeshProUGUI timerdisplay;
    
    void Start()
    {
        CurrentTime = StartTime;
    }

   
    void FixedUpdate()
    {
        CurrentTime -= Time.fixedDeltaTime;
        timerdisplay.text = "" + TimeSpan.FromSeconds(CurrentTime).Minutes.ToString("00") + " : " + TimeSpan.FromSeconds(CurrentTime).Seconds.ToString("00");
    }

    public void subtractTime(float amount)
    {
        CurrentTime -= amount;
    }
}
