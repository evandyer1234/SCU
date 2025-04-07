using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickandDrag : ClickEvent
{
    bool isTesting = true;
    
    bool selected = false;
    [SerializeField] internal bool yonly = false;

    [SerializeField] internal float minY, maxY;

    private Vector3 _positionOffset;

    
    public virtual void Update()
    {
        if (Input.GetMouseButtonUp(0) && selected) 
        {
            selected = false;
            Release();
        }
        if (selected)
        {
            FollowMouse();
        }
    }
    
    public virtual void Release()
    {

    }

    public virtual void FollowMouse()
    {
        Vector3 pos  = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) - _positionOffset;

        if (yonly)
        {
            if (pos.y <= maxY && pos.y >= minY)
            {
                transform.position = new Vector3(transform.position.x, pos.y, 0);
            }
        }
        else
        {
            transform.position = new Vector3(pos.x, pos.y, 0);
        }
        
    }

    public virtual void OnSelected()
    {
        selected = true;

        //calculate position offset
        Vector3 cursorPos  = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        _positionOffset = cursorPos - transform.position;

    }
}
