using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHoverOutline : MonoBehaviour
{
    public HoverOutline hoverOutline;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
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
            hoverOutline.StartHover();
        }
    }

    void OnMouseExit()
    {
        
        if(_spriteRenderer.enabled == true)
        {
            hoverOutline.EndHover();
        }
    }
}
