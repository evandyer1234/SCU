using UnityEngine;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour
{
    public UnityEvent OnClick;
    
    
    public void Clicked()
    {
        OnClick.Invoke();
    }
}
