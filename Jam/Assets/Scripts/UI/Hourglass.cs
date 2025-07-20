using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hourglass : MonoBehaviour
{
    [SerializeField] Slider Top;
    [SerializeField] Slider Bottom;
    [SerializeField] GameObject particles;
    public float Glassvalue;
    public float Maxvalue;

    
    void Start()
    {
        
    }

    public void SetupHourglass(float starttime)
    {
        Maxvalue = starttime;
        Top.maxValue = starttime;
        Bottom.maxValue = starttime;
        Glassvalue = starttime;
    }

    
    void Update()
    {
        Top.value = Glassvalue;
        Bottom.value = Maxvalue - Glassvalue;
        //Debug.Log(Maxvalue - Glassvalue);
        //Glassvalue += Time.fixedDeltaTime * .5f;
        if (Top.value <= 0)
        {
            particles.SetActive(false);
        }
    }
}
