using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class KidneyCutArea : MonoBehaviour
    {
        [SerializeField] private MG_Abdomen abdomenRef;
        [SerializeField] private SpriteRenderer hoverOutlineRef;
        [SerializeField] private bool isLeftKidney;

        private PauseMenuManager _pauseMenuManager;
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
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
        }
        
        private void OnMouseOver()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            
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
            var kidneyMovable = this.isLeftKidney ? abdomenRef.GetLeftKidneyRef() : abdomenRef.GetRightKidneyRef();
            if (kidneyMovable.IsCorrupted())
            {
                kidneyMovable.CutConnection();    
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
