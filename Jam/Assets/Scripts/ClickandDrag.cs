using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClikcandDrag : ClickEvent
{
    bool selected = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) 
        {
            selected = false;
            Release();
        }
    }
    
    public virtual void Release()
    {

    }
}
