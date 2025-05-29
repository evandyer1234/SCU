using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class StomachCutArea : MonoBehaviour
    {
        [SerializeField] private StomachMovable stomachMovableRef;
        [SerializeField] private SpriteRenderer hoverOutlineRef;
        [SerializeField] private bool isTopCutArea;
        
        private SCUInputAction _scuInputAction;
        private bool isConnected = true;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
        }
        
        private void OnMouseOver()
        {
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }

            if (isConnected)
            {
                hoverOutlineRef.enabled = true;    
            }
        }

        private void OnMouseExit()
        {
            hoverOutlineRef.enabled = false;
        }
        
        private void MouseLeftClick()
        {
            if (isTopCutArea)
            {
                stomachMovableRef.CutTopConnection();    
            }
            else
            {
                stomachMovableRef.CutBottomConnection();
            }

            isConnected = false;
            hoverOutlineRef.enabled = false;
        }
    }
}