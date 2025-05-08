using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;

namespace Subjects
{
    public class CorruptionMark : MonoBehaviour
    {
        [SerializeField, Tooltip("The outline of the mark triggering on hover")] 
        private GameObject corruptionOutline;

        [SerializeField, Tooltip("The Minigame Reference to start the game")] 
        public GameObject minigameRef;

        [SerializeField, Tooltip("The Reference of the lens to allow triggering the minigame only when hovered")] 
        public GameObject lensRef;
        
        private bool miniGameLaunched = false;
        
        private SCUInputAction _scuInputAction;

        private MagnifyingGlassShadow glassShadowRef;
        
        private MiniGameManager _miniGameManager;
        private Color inactiveColor = new Color32(70, 70, 70, 255);
        private Color activeColor;
        
        public void Awake()
        {
            corruptionOutline.SetActive(false);
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            
            glassShadowRef = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAGNIFYING_GLASS_SHADOW)
                .GetComponent<MagnifyingGlassShadow>();
            _miniGameManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINIGAME_MANAGER)
                .GetComponent<MiniGameManager>();
            activeColor = GetComponent<SpriteRenderer>().color;
        }

        public void FixedUpdate()
        {
            PaintCorruptionMarkByActivity();
        }

        void OnMouseOver()
        {
            if (!glassShadowRef.GetMagnifyingGlassInUse()) return;
            if (miniGameLaunched) return;
            
            if (!corruptionOutline.activeInHierarchy)
            {
                corruptionOutline.SetActive(true);
            }
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }
        }

        private void MouseLeftClick()
        {
            if (!glassShadowRef.GetMagnifyingGlassInUse()) return;
            if (miniGameLaunched) return;   
            List<Collider2D> lensColliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x/2)
                .Where(col => 
                    col.gameObject.CompareTag(NamingConstants.TAG_LENS_MIDDLE)
                    || col.gameObject.CompareTag(NamingConstants.TAG_LENS_LEFT)
                    || col.gameObject.CompareTag(NamingConstants.TAG_LENS_RIGHT)
                    ).ToList();
            
            if (IsLensHovering(lensColliders))
            {
                LaunchMinigame();
            }
        }

        private bool IsLensHovering(List<Collider2D> colliders)
        {
            return colliders.Any(col => col.gameObject.CompareTag(lensRef.tag));
        }

        private void LaunchMinigame()
        {
            if (!glassShadowRef.GetMagnifyingGlassInUse()) return;
            
            minigameRef.GetComponent<MiniGameBase>().StartMinigame(gameObject);
            miniGameLaunched = true;

            glassShadowRef.SetMagnifyingGlassInUse(false);
        }
        
        void OnMouseExit()
        {
            if (corruptionOutline.activeInHierarchy)
            {
                corruptionOutline.SetActive(false);
            }
        }
        
        private void PaintCorruptionMarkByActivity()
        {
            if (_miniGameManager.IsAnyMinigameRunning())
            {
                GetComponent<SpriteRenderer>().color = inactiveColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = activeColor;
            }
        }
    }
}
