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
        private GameObject minigameRef;

        [SerializeField, Tooltip("The Reference of the lens to allow triggering the minigame only when hovered")] 
        private GameObject lensRef;
        
        private bool miniGameLaunched = false;
        
        public void Awake()
        {
            corruptionOutline.SetActive(false);
        }

        void OnMouseOver()
        {
            if (miniGameLaunched) return;
            
            if (!corruptionOutline.activeInHierarchy)
            {
                corruptionOutline.SetActive(true);
            }

            if(MouseInput.LeftClick()) MouseLeftClick();
        }

        private void MouseLeftClick()
        {
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
            minigameRef.SetActive(true);
            miniGameLaunched = true;
            MagnifyingGlassShadow mgs = GameObject
                .FindGameObjectWithTag(NamingConstants.TAG_MAGNIFYING_GLASS_SHADOW).GetComponent<MagnifyingGlassShadow>();
            mgs.SetMagnifyingGlassInUse(false);
        }
        
        void OnMouseExit()
        {
            if (corruptionOutline.activeInHierarchy)
            {
                corruptionOutline.SetActive(false);
            }
        }
    }
}
