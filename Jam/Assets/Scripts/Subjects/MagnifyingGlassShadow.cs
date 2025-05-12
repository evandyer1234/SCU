using Helpers;
using UI;
using UnityEngine;

namespace Subjects
{
    public class MagnifyingGlassShadow : MonoBehaviour
    {
        [SerializeField, Tooltip("The main sprite of the Magnifying Glass Object")] 
        private SpriteRenderer spriteGlassReference;
        
        [SerializeField, Tooltip("The main sprite of the Magnifying Glass Switch Object")] 
        private SpriteRenderer spriteSwitchReference;

        [SerializeField, Tooltip("The canvas holding all interactable magnifying glass objects")]
        private Canvas canvasMagnifyingGlass;

        [SerializeField, Tooltip("The hint of the shadow when not beeing used for a while")] 
        private InteractionHint shadowHint;
        
        private bool IsMagnifyingGlassInUse = false;
        
        private SCUInputAction _scuInputAction;
        
        // interaction hint
        private int hintCountdown = 250;
        private int resetHintCountdown = 250;
        
        // reset canvas
        private Vector3 resetGlassPos;
        private Vector3 resetSwitchPos;
        private Vector3 resetMiddleLensPos;
        private Vector3 resetLeftLensPos;
        private Vector3 resetRightLensPos;

        private MiniGameManager _miniGameManager;
        
        /** ****************************************************
         * **************** UNITY INTERFACE ********************
         * *****************************************************
         */
        
        public void Awake()
        {
            SpriteRenderer magnifyingGlassSprite = spriteGlassReference.GetComponent<SpriteRenderer>();
            magnifyingGlassSprite.enabled = false;
            SpriteRenderer switchReference = spriteSwitchReference.GetComponent<SpriteRenderer>();
            switchReference.enabled = false;
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();

            _miniGameManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINIGAME_MANAGER)
                .GetComponent<MiniGameManager>();
            
            RememberOriginalMagnifyingGlassElementPositions();
        }
        
        private void FixedUpdate()
        {

            
            HandleInitialUsageHintAnimation();
            HandleResetHintAnimation();
        }
        
        private void OnMouseOver()
        {
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                HandleLeftClick();
            }
        }

        private void HandleLeftClick()
        {
            if (IsMagnifyingGlassInUse)
            {
                ResetMagnifyingGlassPositionIfNeeded();
            }
            else
            {
                IsMagnifyingGlassInUse = true;
                SpriteRenderer magnifyingGlassSprite = spriteGlassReference.GetComponent<SpriteRenderer>();
                magnifyingGlassSprite.enabled = true;
                SpriteRenderer switchReference = spriteSwitchReference.GetComponent<SpriteRenderer>();
                switchReference.enabled = true; 
            }
        }

        /** ****************************************************
         * **************** PUBLIC METHODS *********************
         * *****************************************************
         */
        
        public bool GetMagnifyingGlassInUse()
        {
            return IsMagnifyingGlassInUse;
        }

        public void SetMagnifyingGlassInUse(bool value)
        {
            IsMagnifyingGlassInUse = value;
        }
        
        /** ****************************************************
         * **************** PRIVATE METHODS ********************
         * *****************************************************
         */
        
        private void RememberOriginalMagnifyingGlassElementPositions()
        {
            resetGlassPos = spriteGlassReference.transform.position;
            resetSwitchPos = spriteSwitchReference.transform.position;
            var mg = spriteGlassReference.GetComponent<MagnifyingGlass>();
            resetMiddleLensPos = mg.middleLensReference.transform.position;
            resetLeftLensPos = mg.leftLensReference.transform.position;
            resetRightLensPos = mg.rightLensReference.transform.position;
        }
        
        private void ResetMagnifyingGlassPositionIfNeeded()
        {
            if (spriteSwitchReference.transform.position.y < -11)
            {
                spriteGlassReference.transform.position = resetGlassPos;
                spriteSwitchReference.transform.position = resetSwitchPos;
                var mg = spriteGlassReference.GetComponent<MagnifyingGlass>();
                mg.middleLensReference.transform.position = resetMiddleLensPos;
                mg.leftLensReference.transform.position = resetLeftLensPos;
                mg.rightLensReference.transform.position = resetRightLensPos;
            }
        }
        
        private void HandleInitialUsageHintAnimation()
        {
            if (!IsMagnifyingGlassInUse && !_miniGameManager.IsAnyMinigameRunning())
            {
                hintCountdown--;
                if (hintCountdown <= 0)
                {
                    if (!IsMagnifyingGlassInUse && !_miniGameManager.IsAnyMinigameRunning())
                    {
                        shadowHint.TriggerAnimation();
                        hintCountdown = 350;
                    }
                    else
                    {
                        hintCountdown = 0;
                    }
                }
            }
            else
            {
                hintCountdown = 350;
            }
        }

        private void HandleResetHintAnimation()
        {
            if (spriteSwitchReference.transform.position.y < -11)
            {
                resetHintCountdown--;
                if (resetHintCountdown <= 0)
                {
                    if (spriteSwitchReference.transform.position.y < -11)
                    {
                        shadowHint.TriggerAnimation();
                        resetHintCountdown = 250;
                    }
                    else
                    {
                        resetHintCountdown = 0;
                    }
                }
            }
            else
            {
                resetHintCountdown = 250;
            }
        }
    }
}