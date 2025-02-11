using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MG_Pump : MiniGameBase
{
    [SerializeField] Slider pressure;
    [SerializeField] Slider timeremaining;
    [SerializeField] float goalnum;
    [SerializeField] float variance;
    [SerializeField] float downrate;
    [SerializeField] float pumpamount;
    [SerializeField] float timepunishment;
    [SerializeField] float length;
    float currentvalue;
    float currentrate;
    
    
    private AudioSource _audioSource;
    [Header("Audio")]
    [SerializeField] private AudioClip[] _pumpSounds = new AudioClip[2];

   

    public override void Start()
    {
        base.Start();
        pressure.maxValue = 100f;
        currentvalue = goalnum;
        gameModeManager = EventSystem.current.gameObject.GetComponent<GameModeManager>();
        timeremaining.maxValue = length;
        
        _audioSource = GetComponent<AudioSource>();
    }

    
    void FixedUpdate()
    {
        timeremaining.value = length;
        pressure.value = currentvalue;
        currentvalue -= Time.fixedDeltaTime * downrate;
        //currentvalue += currentrate;

        if (currentvalue > pressure.maxValue)
        {
            currentvalue = pressure.maxValue;
            
        }
        else if (currentvalue < pressure.minValue)
        {
            currentvalue = pressure.minValue;
            
        }

        if (currentvalue > (goalnum + variance))
        {
            gameModeManager.subtractTime(timepunishment);
        }
        else if (currentvalue < (goalnum - variance)) 
        {
            gameModeManager.subtractTime(timepunishment);
        }
        else
        {
            length -= Time.fixedDeltaTime;
        }

        if (length <= 0)
        {
            OnSuccess();
        }
    }

    public void PumpAction()
    {
        currentvalue += pumpamount;
        _audioSource.PlayOneShot(_pumpSounds[Random.Range(0, _pumpSounds.Length)]);
    }
}
