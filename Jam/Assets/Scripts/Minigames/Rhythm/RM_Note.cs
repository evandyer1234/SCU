using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RM_Note : MonoBehaviour
{

    public Slider s;
    public float speed;
    public MG_rhythm rm;
    
    void Start()
    {
        s.value = s.maxValue;
    }

   
    void FixedUpdate()
    {
        s.value -= speed * Time.deltaTime;
        if (s.value <= 0)
        {
            rm.miss();
            Destroy(this.gameObject);
        }
    }
}
