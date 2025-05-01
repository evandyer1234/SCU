using Helpers;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PointandClick : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] genericClickSounds = new AudioClip[8];

    private SCUInputAction _scuInputAction;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
    }
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (MouseInput.LeftClicked(_scuInputAction))
        {
            RaycastHit hit;
            Vector2 mousePos = MouseInput.ScreenPosition(_scuInputAction);
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);
                ClickEvent ce = hit.collider.gameObject.GetComponent<ClickEvent>();
                if (ce != null)
                {
                    ce.Clicked();
                }
            }
            else
            {
                // _audioSource.PlayOneShot(genericClickSounds[Random.Range(0, genericClickSounds.Length)]);
            }
        }
        else if (MouseInput.LeftReleased(_scuInputAction))
        {

        }
    }
}