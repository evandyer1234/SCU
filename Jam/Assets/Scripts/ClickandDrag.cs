using Helpers;
using UnityEngine;

public class ClickandDrag : ClickEvent
{
    bool isTesting = true;
    
    bool selected = false;
    [SerializeField] internal bool yonly = false;

    [SerializeField] internal float minY, maxY;

    private Vector3 _positionOffset;

    private SCUInputAction _scuInputAction;

    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
    }

    public virtual void Update()
    {
        if (MouseInput.LeftReleased(_scuInputAction) && selected) 
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
        //CustomCursor.instance.SetDefaultCursor();
    }

    public virtual void FollowMouse()
    {
        Vector3 pos  = GetCameraPosition() - _positionOffset;

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
        //CustomCursor.instance.SetPressedCursor();
        _positionOffset = GetCameraPosition() - transform.position;
    }


    Vector3 GetCameraPosition()
    {
        return MouseInput.WorldPosition(_scuInputAction);
    }
}
