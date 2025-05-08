using Helpers;
using UnityEngine;

public class MG_AlchemyLeaf : ClickandDrag
{
    // Mouse positiono on leaf when it is clicked, used to determine if the leaf should be removed
    Vector3 mouseClickPosition;

    public float pickDistance = 1.0f;

    private SCUInputAction _scuInputAction;
    
    private void Awake()
    {
        _scuInputAction = new SCUInputAction();
        _scuInputAction.UI.Enable();
    }

    private void Update()
    {
        base.Update();
    }

    public override void FollowMouse()
    {
        // Don't have the leaf follow mouse
    }

    public void Release()
    {
        Vector2 mousePos = MouseInput.WorldPosition(_scuInputAction);
        if (Vector3.Distance(mouseClickPosition, mousePos) >= pickDistance)
        {
            Object.Destroy(this.gameObject);
        }
    }

    public override void OnSelected()
    {
        base.OnSelected();
        mouseClickPosition = MouseInput.WorldPosition(_scuInputAction);
    }
}
