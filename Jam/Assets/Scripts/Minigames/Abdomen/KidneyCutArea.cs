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
        private CustomCursor cursorRef;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
        }
        
        private void Start()
        {
            cursorRef = GameObject.FindGameObjectWithTag(NamingConstants.TAG_CUSTOM_CURSOR)
                .GetComponent<CustomCursor>();
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
            cursorRef.SetScalpelSprite();
        }
        
        private void OnMouseExit()
        {
            hoverOutlineRef.enabled = false;
            cursorRef.SetGloveSprite();
        }
        
        private void MouseLeftClick()
        {
            kidneyMovableRef.CutConnection();    
            isConnected = false;
            hoverOutlineRef.enabled = false;
        }
    }
}