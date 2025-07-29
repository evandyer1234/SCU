using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SwitchSprite : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SwitchTo(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
