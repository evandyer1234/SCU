using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class KidneyMovable : MonoBehaviour
    {
        [SerializeField] private KidneyPlaceholder kidneyPlaceholder;
        [SerializeField] private MG_Abdomen abdomenRef;
        [SerializeField] private bool isConnected;
        [SerializeField] public bool isLefKidney;
        
        private Vector3 offsetKidney;
        private bool followMouse;
        private PauseMenuManager _pauseMenuManager;
        private SCUInputAction _scuInputAction;
        
        private Sprite healthyRightKidneySprite;
        private Sprite corruptRightKidneySprite;
        private Sprite healthyLeftKidneySprite;
        private Sprite corruptLeftKidneySprite;
        private bool isCorrupted = false;
        
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
            
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
        }
        
        private void Update()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            
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
            if (_pauseMenuManager.isGamePaused()) return;
            
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }

            if (MouseInput.LeftReleased(_scuInputAction))
            {
                followMouse = false;
                if (!kidneyPlaceholder.isEmpty) return;
                
                var coll = gameObject.GetComponent<PolygonCollider2D>();
                var allColliders = new List<Collider2D>();
                Physics2D.OverlapCollider(coll, new ContactFilter2D(), allColliders);
                
                foreach (var currColl in allColliders)
                {
                    if (currColl.GetComponent<KidneyPlaceholder>() != null)
                    {
                        var collPlaceholder = currColl.GetComponent<KidneyPlaceholder>();
                        if (!isCorrupted && (isLefKidney == collPlaceholder.isLeftKidneyPlaceholder))
                        {
                            transform.position = kidneyPlaceholder.GetLastKnownKidneyPosition();
                            abdomenRef.AssignNewKidney(this);
                            isConnected = true;
                            kidneyPlaceholder.isEmpty = false;
                        }

                        break;
                    }
                }
            }
        }

        public void CutConnection()
        {
            isConnected = false;
            kidneyPlaceholder.SetLastKnownKidneyPosition(gameObject.transform.position);
            kidneyPlaceholder.isLeftKidneyPlaceholder = isLefKidney;
            kidneyPlaceholder.isEmpty = true;
        }

        public void SetCorrupted()
        {
            if (isLefKidney)
            {
                GetComponent<SpriteRenderer>().sprite = corruptLeftKidneySprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = corruptRightKidneySprite;
            }

            isCorrupted = true;
        }
        
        public bool IsCorrupted()
        {
            return isCorrupted;
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
