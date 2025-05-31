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

    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _defaultColor;

        if(_spriteRenderer.sprite == null)
        {
            Debug.LogError("Sprite missing for HoverOutline: " + transform.parent.name);
        }

        GameObject parentGO = transform.parent.gameObject;

        if(!parentGO.TryGetComponent<TriggerHoverOutline>(out TriggerHoverOutline t))
        {
            parentGO.AddComponent<TriggerHoverOutline>();
        }

        parentGO.GetComponent<TriggerHoverOutline>().hoverOutline = this;
    }


    public void StartHover()
    {
        _spriteRenderer.color = _hoverColor;
    }

    public void EndHover()
    {
        _spriteRenderer.color = _defaultColor;
    }
}
