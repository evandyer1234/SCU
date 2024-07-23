using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MG_Pump : MiniGameBase
{
    [SerializeField] Slider slider;
    [SerializeField] float goalnum;
    [SerializeField] float variance;
    [SerializeField] float downrate;
    [SerializeField] float pumpamount;
    [SerializeField] float timepunishment;
    float currentvalue;
    float currentrate;
    GameModeManager gmm;

    void Start()
    {
        slider.maxValue = 100f;
        currentvalue = goalnum;
        gmm = EventSystem.current.gameObject.GetComponent<GameModeManager>();
    }

    
    void FixedUpdate()
    {
        slider.value = currentvalue;
        currentrate -= Time.fixedDeltaTime * downrate;
        currentvalue += currentrate;

        if (currentvalue > slider.maxValue)
        {
            currentvalue = slider.maxValue;
            currentrate = 0;
        }
        else if (currentvalue < slider.minValue)
        {
            currentvalue = slider.minValue;
            currentrate = 0;
        }

        if (currentvalue > (goalnum + variance))
        {
            gmm.subtractTime(timepunishment);
        }
        if (currentvalue < (goalnum - variance)) 
        {
            gmm.subtractTime(timepunishment);
        }
    }

    public void PumpAction()
    {
        currentrate += pumpamount;
    }
}
