using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_AlchemyLeaf : ClickandDrag
{
    // Mouse positiono on leaf when it is clicked, used to determine if the leaf should be removed
    Vector3 mouseClickPosition;

    public float pickDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        base.Update();
    }

    public override void FollowMouse()
    {
        // Don't have the leaf follow mouse
    }

    public override void Release()
    {
        //Debug.Log("Released:");
        //Debug.Log(Vector3.Distance(mouseClickPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        if (Vector3.Distance(mouseClickPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= pickDistance)
        {
            Object.Destroy(this.gameObject);
        }
    }

    public override void OnSelected()
    {
        base.OnSelected();

        mouseClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
