using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MG_Pump : MiniGameBase
{
    [SerializeField] Slider slider;
    [SerializeField] float goalnum;
    [SerializeField] float variance;
    [SerializeField] float downrate;
    [SerializeField] float pumpamount;
    float currentvalue;

    void Start()
    {
        slider.maxValue = 100f;
        currentvalue = goalnum;
        
    }

    
    void FixedUpdate()
    {
        slider.value = currentvalue;
        currentvalue -= Time.fixedDeltaTime * downrate;

        if (currentvalue > (goalnum + variance))
        {

        }
        else if (currentvalue < (goalnum - variance)) 
        {

        }
    }

    public void PumpAction()
    {
        currentvalue += pumpamount;
    }
}
