using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetFaderLevels : MonoBehaviour
{
    // RTPC
    [SerializeField] private AK.Wwise.RTPC fader;
    
    // Slider
    private Slider _slider;

    [SerializeField] private float initialValue = 100f;
    
    // Start is called before the first frame update
    void Awake()
    {
       _slider = gameObject.GetComponent<Slider>();
       
       if (SceneManager.GetActiveScene().buildIndex == 0)
       {
           _slider.value = initialValue;
           SetFader();
       }
       else
       {
           _slider.value = fader.GetValue(null);
           SetFader();
       }
    }

    public void SetFader()
    {
        if (fader != null && _slider != null)
            fader.SetGlobalValue(_slider.value);
    }
}
