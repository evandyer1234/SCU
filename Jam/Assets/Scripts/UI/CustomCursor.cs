using Helpers;
using UnityEngine;
using UnityEditor;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Sprite _defaultCursor;
    [SerializeField] private Sprite _pressedCursor;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector3 _targetPos;

    public static CustomCursor instance;

    private SCUInputAction _scuInputAction;
    
    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
    }
    
    void Start()
    {
        Cursor.visible = false;

        DontDestroyOnLoad(gameObject);
        //SetDefaultCursor();
    }

    void Update()
    {
        _targetPos = MouseInput.WorldPosition(_scuInputAction);
        _targetPos = new Vector3 (_targetPos.x, _targetPos.y, 10f);
        transform.position = _targetPos;

        Cursor.visible = !MouseScreenCheck();
    }

    //https://discussions.unity.com/t/detect-when-mouse-leaves-the-screen-area/630801/3
    bool MouseScreenCheck()
    {

        Vector2 mousePos = MouseInput.ScreenPosition(_scuInputAction);
        #if UNITY_EDITOR
            if (mousePos.x <= 0 || mousePos.y <= 0 || mousePos.x >= Handles.GetMainGameViewSize().x - 1 || mousePos.y >= Handles.GetMainGameViewSize().y - 1)
            {
                return false;
            }
        
        #else
            
            if (mousePos.x <= 0 || mousePos.y <= 0 || mousePos.x >= Screen.width - 1 || mousePos.y >= Screen.height - 1)
            {
            return false;
            }

        #endif
        
        return true;
    }

    /*
    public void SetDefaultCursor()
    {
        _spriteRenderer.sprite = _defaultCursor;
    }

    public void SetPressedCursor()
    {
        _spriteRenderer.sprite = _pressedCursor;
    }
    */
}
