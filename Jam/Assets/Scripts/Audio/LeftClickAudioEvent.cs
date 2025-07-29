using Helpers;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LeftClickAudioEvent : MonoBehaviour
{
    private PauseMenuManager _pauseMenuManager;
    private SCUInputAction _scuInputAction;
    [SerializeField] private AK.Wwise.Event leftClickedEvent;
    [SerializeField] private AK.Wwise.Event leftReleasedEvent;

    void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
        _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
            .GetComponent<PauseMenuManager>();
    }

    private void OnMouseOver()
    {
        if (_pauseMenuManager.isGamePaused()) return;

        if (MouseInput.LeftClicked(_scuInputAction))
        {
            leftClickedEvent.Post(gameObject);
        }

        if (MouseInput.LeftReleased(_scuInputAction))
        {
            leftReleasedEvent.Post(gameObject);
        }
    }
}