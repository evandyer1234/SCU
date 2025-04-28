using Helpers;
using UnityEngine;

namespace Subjects
{
    public class MagnifyingGlass : MonoBehaviour
    {
        [SerializeField, Tooltip("The Canvas needed for Scale reference")] 
        private Canvas canvas;
        
        [SerializeField, Tooltip("The reference to the initial Trigger Glass Shadow")] 
        private MagnifyingGlassShadow glassShadowReference;
        
        [SerializeField, Tooltip("The Switch belonging to the Magnifying Glass")] 
        private GameObject glassSwitchReference;
        
        [SerializeField, Tooltip("The middle lens of the Glass")] 
        private GameObject middleLensReference;
        
        [SerializeField, Tooltip("The left lens of the Glass")] 
        private GameObject leftLensReference;
        
        [SerializeField, Tooltip("The right lens of the Glass")] 
        private GameObject rightLensReference;
        
        // dragging state
        private bool followMouse = false;
        private bool hovered = false;
        private Vector3 offsetGlassSpriteToMouse = Vector3.zero;
        private Vector3 offsetSwitchSpriteToMouse = Vector3.zero;
        private Vector3 offsetMiddleLensToMouse = Vector3.zero;
        private Vector3 offsetLeftLensToMouse = Vector3.zero;
        private Vector3 offsetRightLensToMouse = Vector3.zero;

        private SCUInputAction _scuInputAction;

        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
        }
        
        private void Update()
        {
            if (!glassShadowReference.GetMagnifyingGlassInUse()) return;
            
            if (followMouse)
            {
                Vector3 mouseWorldPos =  MouseInput.WorldPosition(_scuInputAction);
                KeepSpriteRelativeToMouse(gameObject, mouseWorldPos, offsetGlassSpriteToMouse);
                KeepSpriteRelativeToMouse(glassSwitchReference, mouseWorldPos, offsetSwitchSpriteToMouse);
                KeepSpriteRelativeToMouse(middleLensReference, mouseWorldPos, offsetMiddleLensToMouse);
                KeepSpriteRelativeToMouse(leftLensReference, mouseWorldPos, offsetLeftLensToMouse);
                KeepSpriteRelativeToMouse(rightLensReference, mouseWorldPos, offsetRightLensToMouse);
            }
        }

        private void OnMouseOver()
        {
            if (!glassShadowReference.GetMagnifyingGlassInUse()) return;
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }
            if (MouseInput.LeftReleased(_scuInputAction))
            {
                followMouse = false;
            }
            hovered = true;
        }

        private void OnMouseExit()
        {
            hovered = false;
        }
        
        private void MouseLeftClick()
        {
            followMouse = true;
            Vector3 mousepos =  MouseInput.WorldPosition(_scuInputAction);
            offsetGlassSpriteToMouse = (mousepos - transform.position);
            offsetSwitchSpriteToMouse = (mousepos - glassSwitchReference.transform.position) / canvas.scaleFactor;
            offsetMiddleLensToMouse = (mousepos - middleLensReference.transform.position);
            offsetLeftLensToMouse = (mousepos - leftLensReference.transform.position);
            offsetRightLensToMouse = (mousepos - rightLensReference.transform.position);
        }
        
        private void KeepSpriteRelativeToMouse(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            spriteRefGo.transform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, spriteRefGo.transform.position.z);
        }
        
        public GameObject getMiddleLensReference()
        {
            return middleLensReference;
        }

        public GameObject getLeftLensReference()
        {
            return leftLensReference;
        }

        public GameObject getRightLensReference()
        {
            return rightLensReference;
        }
    }
}
