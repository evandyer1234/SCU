using Helpers;
using UnityEngine;

public class HoverTooltip : MonoBehaviour
{
    [SerializeField] private GameObject _tooltip;
    [SerializeField] private TMPro.TMP_Text _tooltipText;
    private CanvasGroup _canvasGroup;
    public float fadeSpeed;

    private bool _fading = false;
    private bool _waitingToDisable = false;

    private float _currentOpacity;
    private float _targetOpacity;
    
    private PauseMenuManager _pauseMenuManager;


    private void Awake()
    {
        _canvasGroup = _tooltip.GetComponent<CanvasGroup>();
        _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
            .GetComponent<PauseMenuManager>();
    }

    private void Start()
    {
        ForceDisable();
    }

    private void Update()
    {
        if (_pauseMenuManager.isGamePaused()) return;
        
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
    
    public void ForceDisable()
    {
        _tooltip.SetActive(false);
    }

    public void SetTooltipText(string tooltipText)
    {
        _tooltipText.text = tooltipText;
    }

    private void OnMouseEnter()
    {
        if (_pauseMenuManager.isGamePaused()) return;
        
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
}
