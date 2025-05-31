using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHoverOutline : MonoBehaviour
{
    public HoverOutline hoverOutline;

    void OnMouseEnter()
    {
        hoverOutline.StartHover();
    }

    void OnMouseExit()
    {
        hoverOutline.EndHover();
    }
}
