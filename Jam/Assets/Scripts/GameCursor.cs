using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCursor : MonoBehaviour
{
    public Texture2D cursorIdle;
    public Texture2D cursorActive;

    public static GameCursor instance;

    private void Start()
    {
        Default();
    }

    public void Default()
    {
        Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.Auto);
    }

    public void Clickable()
    {
        Cursor.SetCursor(cursorActive, Vector2.zero, CursorMode.Auto);
    }
}