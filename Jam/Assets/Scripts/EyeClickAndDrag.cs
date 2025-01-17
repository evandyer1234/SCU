using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeClickAndDrag : ClickandDrag
{
    [SerializeField] private float maxRotation;

    [SerializeField] private SpriteRenderer irisRenderer, reflectionRenderer, eyeRenderer;

    public bool isInfectedEye;
    public bool isOverEyeSlot;

    private Transform eyeSlotTransform;
    
    public MG_Eye _mgEye;

    public override void Update()
    {
        base.Update();
    }

    public override void FollowMouse()
    {
        Vector3 pos  = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (yonly)
        {
            if (pos.y <= maxY && pos.y >= minY)
            {
                transform.position = new Vector3(transform.position.x, pos.y, -2);
            }
            
        }
        else
        {
            transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }

    public override void OnSelected()
    {
        base.OnSelected();

        if (isInfectedEye)
        {
            _mgEye.intactEye.extractionInProgress = true;
        }

        _mgEye.PickupEyeSound();

    }

    public override void Release()
    {
        base.Release();

        if (isInfectedEye)
        {
            transform.parent.gameObject.SetActive(false);
            _mgEye.intactEye.extractionInProgress = false;
            _mgEye.intactEye.otherEyeExtracted = true;
            _mgEye.sc.enabled = true;
        }
        else
        {
           
            if (isOverEyeSlot)
            {
                Debug.Log("hi");
                if (_mgEye.correctsprite == irisRenderer.sprite)
                {
                    _mgEye.OnSuccess();
                }
                else
                {
                    _mgEye.gameModeManager.subtractTime(_mgEye.penalty);
                }
                // CHECK IF IS CORRECT EYE
                // IF SO, PLACE IN SLOT AND FINISH MINIGAME
                eyeRenderer.sortingOrder = 2;
                irisRenderer.sortingOrder = 3;
                reflectionRenderer.sortingOrder = 4;
                
                transform.position = eyeSlotTransform.position;
                // ELSE, TIMER COUNTS DOWN FASTER TEMPORARILY

            }
        }
        
        _mgEye.ReleaseEyeSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EyeSlot")
        {
            isOverEyeSlot = true;
            eyeSlotTransform = other.transform;
        }
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "EyeSlot")
        {
            isOverEyeSlot = true;
            eyeSlotTransform = other.transform;
        }
        else
        {
            isOverEyeSlot = false;
        }
    }
}
