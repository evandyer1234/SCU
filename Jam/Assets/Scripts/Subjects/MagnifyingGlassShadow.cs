using Helpers;
using UnityEngine;

namespace Subjects
{
    public class MagnifyingGlassShadow : MonoBehaviour
    {
        [SerializeField, Tooltip("The main sprite of the Magnifying Glass Object")] 
        private SpriteRenderer spriteGlassReference;
        
        [SerializeField, Tooltip("The main sprite of the Magnifying Glass Switch Object")] 
        private SpriteRenderer spriteSwitchReference;

        private bool IsMagnifyingGlassInUse = false;
        
        private SCUInputAction _scuInputAction;
        
        public void Awake()
        {
            SpriteRenderer magnifyingGlassSprite = spriteGlassReference.GetComponent<SpriteRenderer>();
            magnifyingGlassSprite.enabled = false;
            SpriteRenderer switchReference = spriteSwitchReference.GetComponent<SpriteRenderer>();
            switchReference.enabled = false;
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
        }
        
        private void OnMouseOver()
        {
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                HandleLeftClick();
            }
        }

        private void HandleLeftClick()
        {
            if (IsMagnifyingGlassInUse) return;
            
            IsMagnifyingGlassInUse = true;
            SpriteRenderer magnifyingGlassSprite = spriteGlassReference.GetComponent<SpriteRenderer>();
            magnifyingGlassSprite.enabled = true;
            SpriteRenderer switchReference = spriteSwitchReference.GetComponent<SpriteRenderer>();
            switchReference.enabled = true;
        }

        public bool GetMagnifyingGlassInUse()
        {
            return IsMagnifyingGlassInUse;
        }

        public void SetMagnifyingGlassInUse(bool value)
        {
            IsMagnifyingGlassInUse = value;
        }
    }
}