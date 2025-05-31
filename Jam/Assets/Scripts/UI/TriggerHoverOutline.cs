using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class TriggerHoverOutline : MonoBehaviour
{
    public HoverOutline hoverOutline;

    private SpriteRenderer _spriteRenderer;
    private SCUInputAction _scuInputAction;

    private bool _pressed = false;


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


    void Update()
    {
        if (!_pressed) return;

        //check if left is released while NOT hovering over the sprite for objects that don't follow the mouse, e.g. glass shadow
        if(MouseInput.LeftReleased(_scuInputAction))
        {
            _pressed = false;
            hoverOutline.Idle();
        }
    }


    void OnMouseOver()
    {
        if (MouseInput.LeftClicked(_scuInputAction))
        {
            OnPressed();
        }
        else if(MouseInput.LeftReleased(_scuInputAction))
        {
            _pressed = false;
            hoverOutline.Hover();
        }
    }


    void OnMouseEnter()
    {
        if(_spriteRenderer.enabled == true && !_pressed)
        {
            hoverOutline.Hover();
        }
    }


    void OnMouseExit()
    {
        if(_spriteRenderer.enabled == true && !_pressed)
        {
            hoverOutline.Idle();
        }
    }


    void OnPressed()
    {
        if(_spriteRenderer.enabled == true && !_pressed)
        {
            _pressed = true;
            hoverOutline.Pressed();
        }
    }
}
