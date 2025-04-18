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
        
        public void Awake()
        {
            SpriteRenderer magnifyingGlassSprite = spriteGlassReference.GetComponent<SpriteRenderer>();
            magnifyingGlassSprite.enabled = false;
            SpriteRenderer switchReference = spriteSwitchReference.GetComponent<SpriteRenderer>();
            switchReference.enabled = false;
        }
        
        private void OnMouseOver()
        {
            if(MouseInput.LeftClick()) HandleLeftClick();
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
    }
}