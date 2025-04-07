using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MG_Stitch : MiniGameBase
{
    [SerializeField] Transform top, bottom, test;
    float angle;
    [SerializeField] LineRenderer lineRendererprefab;
    [SerializeField] GameObject needle;

    bool stringcomplete = true;

    public override void Start()
    {
        base.Start();
       

    }
    
    public void NewString()
    {
        if (stringcomplete)
        {
           
        }
    }

}
