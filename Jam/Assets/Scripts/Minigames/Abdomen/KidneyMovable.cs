using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class KidneyMovable : MonoBehaviour
    {
        [SerializeField] private bool isConnected;
        [SerializeField] private bool isLefKidney;
        
        private Vector3 offsetKidney;
        private bool followMouse;
        private SCUInputAction _scuInputAction;
        
        private Sprite healthyRightKidneySprite;
        private Sprite corruptRightKidneySprite;
        private Sprite healthyLeftKidneySprite;
        private Sprite corruptLeftKidneySprite;
        
        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            healthyRightKidneySprite = FileLoader.GetSpriteByName(FileConstants.SPR_KIDNEY_R_HEALTHY);
            healthyLeftKidneySprite = FileLoader.GetSpriteByName(FileConstants.SPR_KIDNEY_L_HEALTHY);
            corruptRightKidneySprite = FileLoader.GetSpriteByName(FileConstants.SPR_KIDNEY_R_CORRUPT);
            corruptLeftKidneySprite = FileLoader.GetSpriteByName(FileConstants.SPR_KIDNEY_L_CORRUPT);
            if (isLefKidney)
            {
                GetComponent<SpriteRenderer>().sprite = healthyLeftKidneySprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = healthyRightKidneySprite;
            }
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

        public void SetCorruptSprite()
        {
            if (isLefKidney)
            {
                GetComponent<SpriteRenderer>().sprite = corruptLeftKidneySprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = corruptRightKidneySprite;
            }
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
