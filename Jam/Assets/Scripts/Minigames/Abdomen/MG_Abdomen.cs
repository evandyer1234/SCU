using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class MG_Abdomen : MonoBehaviour
    {
        [SerializeField] private StomachMovable stomach;
        [SerializeField] private LiverMovable liver;
        [SerializeField] private GameObject leftKidney;
        [SerializeField] private GameObject rightKidney;
        
        // offsets on drag
        private Vector3 offsetLeftKidney;
        private Vector3 offsetRightKidney;
        
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
                KeepSpriteRelativeToMouse(leftKidney, mouseWorldPos, offsetLeftKidney);
                KeepSpriteRelativeToMouse(rightKidney, mouseWorldPos, offsetRightKidney);
            }
        }

        private void OnMouseOver()
        {
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }
            if (MouseInput.LeftReleased(_scuInputAction))
            {
                followMouse = false;
            }
        }

        private void MouseLeftClick()
        {
            Vector3 mousepos =  MouseInput.WorldPosition(_scuInputAction);
            offsetLeftKidney = (mousepos - leftKidney.transform.position);
            offsetRightKidney = (mousepos - rightKidney.transform.position);
        }
        
        private void KeepSpriteRelativeToMouse(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            spriteRefGo.transform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, spriteRefGo.transform.position.z);
        }
    }
}