using UnityEngine;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour
{
    public UnityEvent OnClick;
    public UnityEvent OnRelease;
    
    
    public void Clicked()
    {
        OnClick.Invoke();
    }

    public void Released()
    {
        OnRelease.Invoke();
    }
}
