using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeClickAndDrag : ClickandDrag
{
    [SerializeField] private float maxRotation;

    public bool isInfectedEye;

    public MG_Eye _mgEye;

    public override void Update()
    {
        base.Update();
    }

    public override void FollowMouse()
    {
        Vector3 pos  = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (yonly)
        {
            if (pos.y <= maxY && pos.y >= minY)
            {
                transform.position = new Vector3(transform.position.x, pos.y, -2);
            }
            
        }
        else
        {
            transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }

    public override void OnSelected()
    {
        base.OnSelected();


    }

    public override void Release()
    {
        base.Release();

        if (isInfectedEye)
        {
            transform.parent.gameObject.SetActive(false);
            _mgEye.intactEye.otherEyeExtracted = true;
        }
    }
}
