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
        private Vector3 offsetGlassSpriteToMouse = Vector3.zero;
        private Vector3 offsetSwitchSpriteToMouse = Vector3.zero;
        private Vector3 offsetMiddleLensToMouse = Vector3.zero;
        private Vector3 offsetLeftLensToMouse = Vector3.zero;
        private Vector3 offsetRightLensToMouse = Vector3.zero;

        private void Update()
        {
            if (!glassShadowReference.GetMagnifyingGlassInUse()) return;
            
            if (followMouse)
            {
                Vector3 mousepos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousepos.x - offsetGlassSpriteToMouse.x, mousepos.y - offsetGlassSpriteToMouse.y, transform.position.z);
                glassSwitchReference.transform.position = new Vector3(mousepos.x - offsetSwitchSpriteToMouse.x, mousepos.y - offsetSwitchSpriteToMouse.y, glassSwitchReference.transform.position.z);
                middleLensReference.transform.position = new Vector3(mousepos.x - offsetMiddleLensToMouse.x, mousepos.y - offsetMiddleLensToMouse.y, middleLensReference.transform.position.z);
                leftLensReference.transform.position = new Vector3(mousepos.x - offsetLeftLensToMouse.x, mousepos.y - offsetLeftLensToMouse.y, leftLensReference.transform.position.z);
                rightLensReference.transform.position = new Vector3(mousepos.x - offsetRightLensToMouse.x, mousepos.y - offsetRightLensToMouse.y, rightLensReference.transform.position.z);
            }
        }

        private void OnMouseOver()
        {
            if (!glassShadowReference.GetMagnifyingGlassInUse()) return;
            if(Input.GetMouseButtonDown(0)) MouseLeftClick();
        }

        void OnMouseUp()
        {
            if (!glassShadowReference.GetMagnifyingGlassInUse()) return;
            followMouse = false;
        }

        private void MouseLeftClick()
        {
            followMouse = true;
            Vector3 mousepos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offsetGlassSpriteToMouse = (mousepos - transform.position);
            offsetSwitchSpriteToMouse = (mousepos - glassSwitchReference.transform.position) / canvas.scaleFactor;
            offsetMiddleLensToMouse = (mousepos - middleLensReference.transform.position);
            offsetLeftLensToMouse = (mousepos - leftLensReference.transform.position);
            offsetRightLensToMouse = (mousepos - rightLensReference.transform.position);
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
