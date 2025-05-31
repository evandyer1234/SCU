using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class TriggerHoverOutline : MonoBehaviour
{
    public HoverOutline hoverOutline;

    private SpriteRenderer _spriteRenderer;
    private SCUInputAction _scuInputAction;


    void Start()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();

        if(gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
        {
            _spriteRenderer = sr;
        }
        else
        {
            Debug.LogError("Missing Sprite Renderer: " + transform.name);
            gameObject.SetActive(false);
        }
    }


    void OnMouseEnter()
    {
        if(_spriteRenderer.enabled == true)
        {
            hoverOutline.Hover();
        }
    }


    void OnMouseExit()
    {
        if(_spriteRenderer.enabled == true)
        {
            hoverOutline.Idle();
        }
    }


    void OnMouseOver()
    {
        if (MouseInput.LeftClicked(_scuInputAction))
        {
            hoverOutline.Pressed();
        }
    }
}
