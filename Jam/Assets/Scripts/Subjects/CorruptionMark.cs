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

        [SerializeField, Tooltip("The Minigame Tag name to start respective minigame")] 
        private string minigameTagName;

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
            
            switch (minigameTagName)
            {
                case NamingConstants.TAG_MINIGAME_HEARTSTRING:
                    if (IsLensHovering(lensColliders, NamingConstants.TAG_LENS_LEFT))
                    {
                        LaunchMinigameByTag(NamingConstants.TAG_MINIGAME_HEARTSTRING);
                    }
                    break;
                default:
                    Debug.LogError("No Minigame matched to launch from Corruption Mark. Check if you have a typo on the Minigame Tag or whether you renamed it recently!");
                    break;
            }
        }

        private bool IsLensHovering(List<Collider2D> colliders, string lensTag)
        {
            return colliders.Any(col => col.gameObject.CompareTag(lensTag));
        }

        private void LaunchMinigameByTag(string minigameTagName)
        {
            MiniGameManager mgm = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINI_GAME_MANAGER)
                .GetComponent<MiniGameManager>();
            mgm.ActivateMinigameByTag(minigameTagName);
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