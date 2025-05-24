using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class HoverTooltip : MonoBehaviour
{   
    [SerializeField] private GameObject _tooltip;
    [SerializeField] private CanvasGroup _canvasGroup;
    public float fadeSpeed;

    private bool _fading = false;
    private bool _waitingToDisable = false;

    private float _currentOpacity;
    private float _targetOpacity;


    private void Start()
    {
        ForceDisable();
    }

    private void Update()
    {
        if(_fading)
        {
            _currentOpacity = Mathf.MoveTowards(_currentOpacity, _targetOpacity, fadeSpeed * Time.deltaTime);
            
            _canvasGroup.alpha = _currentOpacity;

            if(Mathf.Approximately(_currentOpacity, _targetOpacity))
            {
                _fading = false;

                if(_waitingToDisable)
                {
                    _tooltip.SetActive(false);
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        _tooltip.SetActive(true);
        _targetOpacity = 1f;
        _currentOpacity = 0f;
        _fading = true;
        _waitingToDisable = false;
        _canvasGroup.alpha = _currentOpacity;
    }

    private void OnMouseExit()
    {
        _targetOpacity = 0f;
        _currentOpacity = 1f;
        _fading = true;
        _waitingToDisable = true;
        _canvasGroup.alpha = _currentOpacity;
    }

    public void ForceDisable()
    {
        _tooltip.SetActive(false);
    }
}
