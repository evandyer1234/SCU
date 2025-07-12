using Helpers;
using UnityEngine;

public class LeftClickAudioEvent : MonoBehaviour
{
    private PauseMenuManager _pauseMenuManager;
    private SCUInputAction _scuInputAction;
    [SerializeField] private AK.Wwise.Event mouseOverEvent;
    [SerializeField] private AK.Wwise.Event leftClickedEvent;
    [SerializeField] private AK.Wwise.Event leftReleasedEvent;
    [SerializeField] private bool playDuringPause = false;

    void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
        _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
            .GetComponent<PauseMenuManager>();
    }

    private void OnEnable()
    {
        Physics.SyncTransforms();
    }

    private void OnMouseOver()
    {
        if (_pauseMenuManager.isGamePaused() && !playDuringPause) return;

        if (MouseInput.LeftClicked(_scuInputAction) && leftClickedEvent != null)
        {
            leftClickedEvent.Post(gameObject);
        }

        if (MouseInput.LeftReleased(_scuInputAction) && leftReleasedEvent != null)
        {
            leftReleasedEvent.Post(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse Enter: " + gameObject.name);
        if (_pauseMenuManager.isGamePaused() && !playDuringPause) return;
        
        if (mouseOverEvent != null)
        {
            mouseOverEvent.Post(gameObject);
        }
    }
}