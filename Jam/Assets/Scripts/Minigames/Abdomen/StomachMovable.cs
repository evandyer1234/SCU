using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Minigames.Abdomen
{
    public class StomachMovable : MonoBehaviour
    {
        [SerializeField] private GameObject stomachConnection;
        [SerializeField] private StomachPlaceholder _stomachPlaceholder;
        [SerializeField] private MG_Abdomen abdomenRef;
        [SerializeField] private bool isConnected;
        
        private Sprite healthyStomachSprite;
        private Sprite corruptStomachSprite;
        private bool isCorrupted = false;
        
        private PauseMenuManager _pauseMenuManager;
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
            InitializeStomachOffsets(gameObject.transform.position);
            healthyStomachSprite = FileLoader.GetSpriteByName(FileConstants.SPR_STOMACH_HEALTHY);
            corruptStomachSprite = FileLoader.GetSpriteByName(FileConstants.SPR_STOMACH_CORRUPT);
            GetComponent<SpriteRenderer>().sprite = healthyStomachSprite;
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
        }
        
        private void Update()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            
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
            if (_pauseMenuManager.isGamePaused()) return;
            
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }

            if (MouseInput.LeftReleased(_scuInputAction))
            {
                followMouse = false;
                if (!_stomachPlaceholder.isEmpty) return;
                
                var coll = gameObject.GetComponent<PolygonCollider2D>();
                var allColliders = new List<Collider2D>();
                Physics2D.OverlapCollider(coll, new ContactFilter2D(), allColliders);

                foreach (var currColl in allColliders)
                {
                    if (currColl.GetComponent<StomachPlaceholder>() != null)
                    {
                        if (!isCorrupted)
                        {
                            transform.position = _stomachPlaceholder.GetLastKnownStomachPosition();
                            InitializeStomachOffsets(_stomachPlaceholder.GetLastKnownStomachRootPosition());
                            abdomenRef.AssignNewStomach(this);
                            isConnected = true;
                            _stomachPlaceholder.isEmpty = false;
                        }

                        break;
                    }
                }
            }
        }

        public void CutTopConnection()
        {
            isTopConnectionCut = true;
            if (isBottomConnectionCut)
            {
                _stomachPlaceholder.SetLastKnownStomachPosition(transform.position);
                _stomachPlaceholder.SetLastKnownStomachRootPosition(yRootPosition);
                _stomachPlaceholder.isEmpty = true;
                isConnected = false;
            }
        }

        public void CutBottomConnection()
        {
            isBottomConnectionCut = true;
            if (isTopConnectionCut)
            {
                _stomachPlaceholder.SetLastKnownStomachPosition(transform.position);
                _stomachPlaceholder.SetLastKnownStomachRootPosition(yRootPosition);
                _stomachPlaceholder.isEmpty = true;
                isConnected = false;
            }
        }
        
        public void SetCorrupted()
        {
            GetComponent<SpriteRenderer>().sprite = corruptStomachSprite;
            isCorrupted = true;
        }
        
        public bool IsCorrupted()
        {
            return isCorrupted;
        }
        
        private void MouseLeftClick()
        {
            Vector3 mousepos =  MouseInput.WorldPosition(_scuInputAction);
            offsetStomach = (mousepos - gameObject.transform.position);
            followMouse = true;
        }
        
        private void InitializeStomachOffsets(Vector3 rootPos)
        {
            yRootPosition = rootPos;
            yConnToStomachDiff = stomachConnection.transform.position.y - gameObject.transform.position.y;
        }
        
        private float KeepSpriteRelativeToMouseY(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            var spritePos = spriteRefGo.transform.position;
            var yDiffFromRoot = yRootPosition.y - mousePos.y + offset.y;
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