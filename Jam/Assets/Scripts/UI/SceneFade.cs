using System;
using UnityEngine;

public class SceneFade : MonoBehaviour
{   
    public float fadeSpeed;

    [SerializeField] private CanvasGroup _canvasGroup;

    private bool _transitionExecuted = false;
    private float _currentOpacity;
    private float _targetOpacity;

    private Action _sceneTransitionCallback;

    void Start()
    {
        FadeIn();
    }

    void Update()
    {
        _currentOpacity = Mathf.MoveTowards(_currentOpacity, _targetOpacity, fadeSpeed * Time.deltaTime);
        _canvasGroup.alpha = _currentOpacity;

        if(Mathf.Approximately(_currentOpacity, _targetOpacity) && !_transitionExecuted)
        {
            if (Mathf.Approximately(_targetOpacity, 1) && _sceneTransitionCallback != null)
            {
                _sceneTransitionCallback.Invoke();
            }

            _transitionExecuted = true;
        }
    }

    [ContextMenu("Fade In")]
    void FadeIn()
    {
        _currentOpacity = 1f;
        _targetOpacity = 0f;
        _transitionExecuted = false;
    }

    [ContextMenu("Fade Out")]
    public void FadeOut(Action sceneTransitionCallback = null)
    {
        _currentOpacity = 0f;
        _targetOpacity = 1f;
        _sceneTransitionCallback = sceneTransitionCallback;
        _transitionExecuted = false;
    }
}
