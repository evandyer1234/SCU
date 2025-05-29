using Helpers;
using UnityEngine;
using UnityEditor;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private GameObject gloveRef;
    [SerializeField] private GameObject scalpelRef;
    
    private SpriteRenderer _currentSpriteRenderer;

    private Vector3 _targetPos;
    public static CustomCursor instance;

    private SCUInputAction _scuInputAction;
    private Sprite scalpelSprite;
    private Sprite gloveSprite;
    private Sprite glovePressedSprite;
    private Sprite _defaultCursor;
    private Sprite _pressedCursor;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
        scalpelSprite = FileLoader.GetSpriteByName(AddressableConstants.SPR_CURSOR_SCALPEL);
        gloveSprite = FileLoader.GetSpriteByName(AddressableConstants.SPR_CURSOR_GLOVE);
        glovePressedSprite = FileLoader.GetSpriteByName(AddressableConstants.SPR_CURSOR_GLOVE_PRESSED);

        SetGloveSprite();
    }
    
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        _targetPos = MouseInput.WorldPosition(_scuInputAction);
        _targetPos = new Vector3 (_targetPos.x, _targetPos.y, 10f);
        transform.position = _targetPos;

        if (MouseInput.IsLeftPressed(_scuInputAction))
        {
            SetPressedCursor();
        }
        else
        {
            SetDefaultCursor();
        }
        
        Cursor.visible = !MouseScreenCheck();
    }

    public void SetScalpelSprite()
    {
        DisableAllCursorRefs();
        scalpelRef.SetActive(true);
        _defaultCursor = scalpelSprite;
        _pressedCursor = scalpelSprite;
        _currentSpriteRenderer = scalpelRef.GetComponent<SpriteRenderer>();
        SetDefaultCursor();
    }

    public void SetGloveSprite()
    {
        DisableAllCursorRefs();
        gloveRef.SetActive(true);
        _defaultCursor = gloveSprite;
        _pressedCursor = glovePressedSprite;
        _currentSpriteRenderer = gloveRef.GetComponent<SpriteRenderer>();
        SetDefaultCursor();
    }

    private void DisableAllCursorRefs()
    {
        gloveRef.SetActive(false);
        scalpelRef.SetActive(false);
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

    private void SetDefaultCursor()
    {
        _currentSpriteRenderer.sprite = _defaultCursor;
    }

    private void SetPressedCursor()
    {
        _currentSpriteRenderer.sprite = _pressedCursor;
    }
}
