using Helpers;
using UnityEngine;

public class Xrayfollow : MonoBehaviour
{
    private SCUInputAction _scuInputAction;

    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = MouseInput.WorldPosition(_scuInputAction);
    }
}
