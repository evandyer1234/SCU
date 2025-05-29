using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class StomachMovable : MonoBehaviour
    {
        [SerializeField] private GameObject stomachConnection;
        [SerializeField] private bool isConnected;
        
        private SCUInputAction _scuInputAction;
        
        // movement related
        private Vector3 offsetStomach;
        private Vector3 yRootPosition;
        private bool followMouse;
        private float yMoveArea = 0.8f;
        private float yConnToStomachDiff;

        // state related
        private bool isTopConnectionCut = false;
        private bool isBottomConnectionCut = false;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            yRootPosition = gameObject.transform.position;
            yConnToStomachDiff = stomachConnection.transform.position.y - gameObject.transform.position.y;
        }
        
        private void Update()
        {
            if (followMouse)
            {
                Vector3 mouseWorldPos =  MouseInput.WorldPosition(_scuInputAction);

                if (isConnected)
                {
                    var newY = KeepSpriteRelativeToMouseY(gameObject, mouseWorldPos, offsetStomach);
                    var stomachConnPos = stomachConnection.transform.position;
                    stomachConnection.transform.position = new Vector3(stomachConnPos.x, yConnToStomachDiff + newY, stomachConnPos.z);
                }
                else
                {
                    KeepSpriteRelativeToMouse(gameObject, mouseWorldPos, offsetStomach);
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

        public void CutTopConnection()
        {
            isTopConnectionCut = true;
            if (isBottomConnectionCut)
            {
                isConnected = false;
            }
        }

        public void CutBottomConnection()
        {
            isBottomConnectionCut = true;
            if (isTopConnectionCut)
            {
                isConnected = false;
            }
        }
        
        private void MouseLeftClick()
        {
            Vector3 mousepos =  MouseInput.WorldPosition(_scuInputAction);
            offsetStomach = (mousepos - gameObject.transform.position);
            followMouse = true;
        }
        
        private float KeepSpriteRelativeToMouseY(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            var spritePos = spriteRefGo.transform.position;
            var yDiffFromRoot = yRootPosition.y - mousePos.y;
            var newY = yDiffFromRoot < -yMoveArea ? (yRootPosition.y + yMoveArea) : (yDiffFromRoot > yMoveArea ? (yRootPosition.y - yMoveArea) : ((mousePos.y) - offset.y));
            spriteRefGo.transform.position = new Vector3(spritePos.x, newY, spritePos.z);
            return newY;
        }
        
        private void KeepSpriteRelativeToMouse(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            spriteRefGo.transform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, spriteRefGo.transform.position.z);
        }
    }
}