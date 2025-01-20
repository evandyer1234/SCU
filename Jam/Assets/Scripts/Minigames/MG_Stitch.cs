using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MG_Stitch : MiniGameBase
{
    [SerializeField] Transform top, bottom, test;
    float angle;

    public override void Start()
    {
        base.Start();
        CalculateAngle();
        //Debug.Log(angle);
        /*
        Vector3 delta = (bottom.position - top.position).normalized;
        Vector3 cross = Vector3.Cross(delta, );
        if (cross.z > 0)
        {
            Debug.Log("right");
        }
        else
        {
            Debug.Log("left"); 
        }
        */

    }
    public void CalculateAngle()
    {
        float x = bottom.position.x - top.position.x;
        float y = bottom.position.y - top.position.y;
        angle = Mathf.Atan2(x, y) * (180 / Mathf.PI);
        if (IsAboveLine(top.position, bottom.position, test.position))
        {
            Debug.Log("On It");
        }
        else
        {
            Debug.Log("pffpfpfp");
        }
    }

    public bool IsAboveLine(Vector3 f, Vector3 s, Vector3 p)
    {
        float m = (s.y - f.y) / (s.x - f.x);
        //float y = m * p.x + top.position.y;
        float y = m * p.x + s.y;
        Debug.Log("y is " + y + " and mouse y is " + p.y);

        if (y < p.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
