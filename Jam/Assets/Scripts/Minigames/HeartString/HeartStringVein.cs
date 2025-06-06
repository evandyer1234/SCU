using Helpers;
using UnityEngine;

namespace Minigames.HeartString
{
    public class HeartStringVein : MonoBehaviour
    {
        [SerializeField, Tooltip("A reference to the managing script of HeartString Minigame")] 
        private GameObject heartStringMinigameRef;
        
        [SerializeField, Tooltip("The outline of the vein for cutting")] 
        private GameObject veinOutline;
        
        [SerializeField, Tooltip("The index of the icon this vein is attached to")] 
        private int iconIndex;
        
        private SCUInputAction _scuInputAction;
        private PauseMenuManager _pauseMenuManager;

        private float hoveredOutlineAlpha = 0.7f;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
            SetOutlineAlpha(0f);
        }
        
        void OnMouseOver()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            if (!veinOutline.activeInHierarchy) return;
            
            SetOutlineAlpha(hoveredOutlineAlpha);
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }
        }

        private void MouseLeftClick()
        {
            heartStringMinigameRef.GetComponent<MG_HeartString>().Selection(iconIndex);
        }

        void OnMouseExit()
        {
            if (!veinOutline.activeInHierarchy) return;

            SetOutlineAlpha(0f);
        }

        private void SetOutlineAlpha(float alpha)
        {
            var spriteRend = veinOutline.GetComponent<SpriteRenderer>();
            var c = spriteRend.color;
            spriteRend.color = new Color(c.r, c.g, c.b, alpha);
        }
    }
}