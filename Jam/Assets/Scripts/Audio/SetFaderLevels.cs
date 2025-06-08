using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFaderLevels : MonoBehaviour
{
    // RTPCs
    [SerializeField] private AK.Wwise.RTPC masterFader;
    [SerializeField] private AK.Wwise.RTPC musicFader;
    [SerializeField] private AK.Wwise.RTPC soundFader;
    
    // Sliders
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        masterFader.SetGlobalValue(100f);
        musicFader.SetGlobalValue(100f);
        soundFader.SetGlobalValue(100f);
    }

    public void SetMasterFader()
    {
        masterFader.SetGlobalValue(masterSlider.value);
    }

    public void SetMusicFader()
    {
        musicFader.SetGlobalValue(musicSlider.value);
    }

    public void SetSoundFader()
    {
        soundFader.SetGlobalValue(soundSlider.value);
    }
}
