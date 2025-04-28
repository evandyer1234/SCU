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

        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            veinOutline.SetActive(false);
        }
        
        void OnMouseOver()
        {
            if (!veinOutline.activeInHierarchy)
            {
                veinOutline.SetActive(true);
            }
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
            if (veinOutline.activeInHierarchy)
            {
                veinOutline.SetActive(false);
            }
        }
    }
}