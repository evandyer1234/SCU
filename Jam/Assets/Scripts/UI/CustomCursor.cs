using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
