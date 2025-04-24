using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomCursor : MonoBehaviour
{
    private Vector3 _targetPos;

    void Start()
    {
        Cursor.visible = false;
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
            if (Input.mousePosition.x == 0 || Input.mousePosition.y == 0 || Input.mousePosition.x >= Handles.GetMainGameViewSize().x - 1 || Input.mousePosition.y >= Handles.GetMainGameViewSize().y - 1)
            {
                return false;
            }
        
        #else
            
            if (Input.mousePosition.x == 0 || Input.mousePosition.y == 0 || Input.mousePosition.x >= Screen.width - 1 || Input.mousePosition.y >= Screen.height - 1)
            {
            return false;
            }

        #endif
        
        return true;
    }
}
