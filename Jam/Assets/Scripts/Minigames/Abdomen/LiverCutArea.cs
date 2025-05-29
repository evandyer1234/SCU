using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class LiverCutArea : MonoBehaviour
    {
        [SerializeField] private LiverMovable liverMovableRef;
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
            liverMovableRef.CutConnection();    
            isConnected = false;
            hoverOutlineRef.enabled = false;
        }
    }
}