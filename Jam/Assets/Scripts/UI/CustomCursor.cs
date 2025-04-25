using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Sprite _defaultCursor;
    [SerializeField] private Sprite _pressedCursor;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector3 _targetPos;

    public static CustomCursor instance;

    void Start()
    {
        Cursor.visible = false;

        //SetDefaultCursor();
    }

    void Update()
    {
        _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _targetPos = new Vector3 (_targetPos.x, _targetPos.y, 10f);
        transform.position = _targetPos;

        Cursor.visible = !MouseScreenCheck();
    }

    //https://discussions.unity.com/t/detect-when-mouse-leaves-the-screen-area/630801/3
    bool MouseScreenCheck()
    {

        #if UNITY_EDITOR
            if (Input.mousePosition.x <= 0 || Input.mousePosition.y <= 0 || Input.mousePosition.x >= Handles.GetMainGameViewSize().x - 1 || Input.mousePosition.y >= Handles.GetMainGameViewSize().y - 1)
            {
                return false;
            }
        
        #else
            
            if (Input.mousePosition.x <= 0 || Input.mousePosition.y <= 0 || Input.mousePosition.x >= Screen.width - 1 || Input.mousePosition.y >= Screen.height - 1)
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
