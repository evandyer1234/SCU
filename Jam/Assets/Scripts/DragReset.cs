using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragReset : ClickandDrag
{
    Vector3 startpos;
    void Start()
    {
        startpos = transform.position;
    }

    
    public override void Release()
    {
        transform.position = startpos;
    }
}
