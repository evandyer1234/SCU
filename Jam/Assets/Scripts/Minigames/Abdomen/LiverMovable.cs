using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class LiverMovable : MonoBehaviour
    {
        [SerializeField] private bool isConnected;

        private SCUInputAction _scuInputAction;
        
        private Sprite healthyLiverSprite;
        private Sprite corruptLiverSprite;
        private bool isCorrupted = false;
        
        private Vector3 offsetLiver;
        private Vector3 yRootPosition;
        private float yMoveArea = 0.3f;
        private bool followMouse;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            yRootPosition = gameObject.transform.position;
            healthyLiverSprite = FileLoader.GetSpriteByName(FileConstants.SPR_LIVER_HEALTHY);
            corruptLiverSprite = FileLoader.GetSpriteByName(FileConstants.SPR_LIVER_CORRUPT);
            GetComponent<SpriteRenderer>().sprite = healthyLiverSprite;
        }
        
        private void Update()
        {
            if (followMouse)
            {
                Vector3 mouseWorldPos =  MouseInput.WorldPosition(_scuInputAction);

                if (isConnected)
                {
                    KeepSpriteRelativeToMouseY(gameObject, mouseWorldPos, offsetLiver);
                }
                else
                {
                    KeepSpriteRelativeToMouse(gameObject, mouseWorldPos, offsetLiver);
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

        public void SetCorrupted()
        {
            GetComponent<SpriteRenderer>().sprite = corruptLiverSprite;
            isCorrupted = true;
        }

        public bool IsCorrupted()
        {
            return isCorrupted;
        }
        
        private void MouseLeftClick()
        {
            Vector3 mousepos =  MouseInput.WorldPosition(_scuInputAction);
            offsetLiver = (mousepos - gameObject.transform.position);
            followMouse = true;
        }
        
        private void KeepSpriteRelativeToMouseY(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            var spritePos = spriteRefGo.transform.position;
            var yDiffFromRoot = yRootPosition.y - mousePos.y;
            var newY = yDiffFromRoot < -yMoveArea ? (yRootPosition.y + yMoveArea) : (yDiffFromRoot > yMoveArea ? (yRootPosition.y - yMoveArea) : ((mousePos.y) - offset.y));
            spriteRefGo.transform.position = new Vector3(spritePos.x, newY, spritePos.z);
        }
        
        private void KeepSpriteRelativeToMouse(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            spriteRefGo.transform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, spriteRefGo.transform.position.z);
        }
    }
}
