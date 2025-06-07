using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class StomachCutArea : MonoBehaviour
    {
        [SerializeField] private MG_Abdomen abdomenRef;
        [SerializeField] private SpriteRenderer hoverOutlineRef;
        [SerializeField] private bool isTopCutArea;
        
        private CustomCursor cursorRef;
        private SCUInputAction _scuInputAction;
        private bool isConnected = true;
        
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
            var stomachMovableRef = abdomenRef.GetStomachRef();
            if (stomachMovableRef.IsCorrupted())
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
            else
            {
                abdomenRef.OnPenalty();
            }
        }
    }
}