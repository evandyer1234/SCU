using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class KidneyMovable : MonoBehaviour
    {
        [SerializeField] private bool isConnected;
        
        private Vector3 offsetKidney;
        private bool followMouse;
        private SCUInputAction _scuInputAction;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
        }
        
        private void Update()
        {
            if (followMouse)
            {
                Vector3 mouseWorldPos =  MouseInput.WorldPosition(_scuInputAction);
                if (!isConnected)
                {
                    KeepSpriteRelativeToMouse(gameObject, mouseWorldPos, offsetKidney);    
                }
            }
            if (MouseInput.LeftReleased(_scuInputAction))
            {
                followMouse = false;
            }
        }
        
        private void OnMouseOver()
        {
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }
        }

        public void CutConnection()
        {
            isConnected = false;
        }
        
        private void MouseLeftClick()
        {
            Vector3 mousepos =  MouseInput.WorldPosition(_scuInputAction);
            offsetKidney = (mousepos - gameObject.transform.position);
            followMouse = true;
        }
        
        private void KeepSpriteRelativeToMouse(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            spriteRefGo.transform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, spriteRefGo.transform.position.z);
        }
    }
}
