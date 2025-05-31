using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HoverOutline : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    [SerializeField] private Color _defaultColor = new Color(1f, 1f, 1f, 0f);
    [SerializeField] private Color _hoverColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private Color _pressedColor;

    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _defaultColor;

        GameObject parentGO = transform.parent.gameObject;

        //update sorting order according to parent
        if(parentGO.TryGetComponent<SpriteRenderer>(out SpriteRenderer parentSR))
        {
            _spriteRenderer.sortingLayerName = parentSR.sortingLayerName;
            _spriteRenderer.sortingOrder = parentSR.sortingOrder - 1;
        }

        if(_spriteRenderer.sprite == null)
        {
            Debug.LogError("Sprite missing for HoverOutline: " + parentGO.transform.name);
        }

        if(!parentGO.TryGetComponent<TriggerHoverOutline>(out TriggerHoverOutline t))
        {
            parentGO.AddComponent<TriggerHoverOutline>();
        }

        parentGO.GetComponent<TriggerHoverOutline>().hoverOutline = this;
    }


    public void Hover()
    {
        _spriteRenderer.color = _hoverColor;
    }


    public void Idle()
    {
        _spriteRenderer.color = _defaultColor;
    }


    public void Pressed()
    {
        _spriteRenderer.color = _pressedColor;
    }
}
