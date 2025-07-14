using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetFaderLevels : MonoBehaviour
{
    // RTPC
    [SerializeField] private AK.Wwise.RTPC fader;
    
    // Slider
    private Slider _slider;
    
    // Start is called before the first frame update
    void Awake()
    {
       _slider = gameObject.GetComponent<Slider>();
       
       if (SceneManager.GetActiveScene().buildIndex == 0)
       {
           _slider.value = 100f;
       }
       else
       {
           _slider.value = fader.GetValue(null);
           SetFader();
       }
    }

    public void SetFader()
    {
        fader.SetGlobalValue(_slider.value);
    }
}
