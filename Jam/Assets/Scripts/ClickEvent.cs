using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour
{
    public UnityEvent OnClick;
    
    
    public void Clicked()
    {
        OnClick.Invoke();
    }

    public void testevent()
    {
        Debug.Log("hoi");
    }
}
