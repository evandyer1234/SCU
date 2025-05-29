using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class KidneyCutArea : MonoBehaviour
    {
        [SerializeField] private KidneyMovable kidneyMovableRef;
        [SerializeField] private SpriteRenderer hoverOutlineRef;

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
            kidneyMovableRef.CutConnection();    
            isConnected = false;
            hoverOutlineRef.enabled = false;
        }
    }
}