using Helpers;
using UI;
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
        public GameObject middleLensReference;
        
        [SerializeField, Tooltip("The left lens of the Glass")] 
        public GameObject leftLensReference;
        
        [SerializeField, Tooltip("The right lens of the Glass")] 
        public GameObject rightLensReference;
        
        [SerializeField, Tooltip("The hint to play if not interacted")] 
        public InteractionHint handleHint;
        
        // dragging state
        private bool followMouse = false;
        private bool hovered = false;
        private Vector3 offsetGlassSpriteToMouse = Vector3.zero;
        private Vector3 offsetSwitchSpriteToMouse = Vector3.zero;
        private Vector3 offsetMiddleLensToMouse = Vector3.zero;
        private Vector3 offsetLeftLensToMouse = Vector3.zero;
        private Vector3 offsetRightLensToMouse = Vector3.zero;

        // interaction hint
        private bool usedOnce = false;
        private int hintCountdown = 250;
        
        private SCUInputAction _scuInputAction;
        private MiniGameManager _miniGameManager;
        private Color inactiveColor = new Color32(128, 128, 128, 255);

        private void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            _miniGameManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINIGAME_MANAGER)
                .GetComponent<MiniGameManager>();
        }
        
        private void Update()
        {
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

        private void FixedUpdate()
        {
            PaintMagnifyingGlassByActivity();
            
            if (!glassShadowReference.GetMagnifyingGlassInUse()) return;
            
            AnimateClickHint();
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
            usedOnce = true;
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
        
        private void AnimateClickHint()
        {
            hintCountdown--;
            if (hintCountdown <= 0)
            {
                if (!usedOnce)
                {
                    handleHint.TriggerAnimation();
                    usedOnce = true;
                }
                hintCountdown = 0;
            }
        }
        
        private void PaintMagnifyingGlassByActivity()
        {
            if (_miniGameManager.IsAnyMinigameRunning())
            {
                GetComponent<SpriteRenderer>().color = inactiveColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
