using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFade : MonoBehaviour
{   
    public float fadeSpeed;

    [SerializeField] private CanvasGroup _canvasGroup;

    private float _currentOpacity;
    private float _targetOpacity;

    void Start()
    {
        FadeIn();
    }

    void Update()
    {
        _currentOpacity = Mathf.MoveTowards(_currentOpacity, _targetOpacity, fadeSpeed * Time.deltaTime);

        _canvasGroup.alpha = _currentOpacity;

        if(_currentOpacity == _targetOpacity)
        {
            gameObject.SetActive(false);
        }
    }

    [ContextMenu("Fade In")]
    void FadeIn()
    {
        _currentOpacity = 1f;
        _targetOpacity = 0f;
    }

    [ContextMenu("Fade Out")]
    public void FadeOut()
    {
        _currentOpacity = 0f;
        _targetOpacity = 1f;
    }
}
