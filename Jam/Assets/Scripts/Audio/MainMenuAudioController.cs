using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioController : MonoBehaviour
{
    // RTPCs
    [SerializeField] private AK.Wwise.RTPC isLevelSelect;

    void Start()
    {
        isLevelSelect.SetGlobalValue(0f);
    }

    public void IsLevelSelect(bool value)
    {
        isLevelSelect.SetGlobalValue(value ? 1f : 0f);
    }
}
