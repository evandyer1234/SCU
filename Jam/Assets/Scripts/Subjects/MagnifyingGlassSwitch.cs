using System;
using Helpers;
using UnityEngine;

namespace Subjects
{
    public class MagnifyingGlassSwitch : MonoBehaviour
    {
        [SerializeField, Tooltip("The reference to the initial Trigger Glass Shadow")] 
        private MagnifyingGlassShadow glassShadowReference;
        
        [SerializeField, Tooltip("The whole Magnifying Glass Object")] 
        private SpriteRenderer magnifyingGlassReference;
        
        [SerializeField, Tooltip("The main Magnifying Glass Sprite")] 
        private SpriteRenderer glassMainSpriteRenderer;
        
        [SerializeField, Tooltip("The Magnifying Glass Shadow Sprite")]  
        private SpriteRenderer glassShadowSpriteRenderer;
        
        [SerializeField, Tooltip("The Sprite of the Switch laying above the handle")]
        private SpriteRenderer glassSwitchSpriteRenderer;
        
        // Addressable sprite names to match
        private static string GLASS_SHADOW_OPEN = "glass_shadow_open";
        private static string GLASS_SHADOW_CLOSED = "glass_shadow_closed";
        private static string GLASS_OPEN = "glass_open";
        private static string GLASS_CLOSED = "glass_closed";
        private static string GLASS_SWITCH_OPEN = "glass_switch_open";
        private static string GLASS_SWITCH_CLOSED = "glass_switch_closed";
        
        // Loading these sprites once
        private Sprite glassShadowOpen;
        private Sprite glassShadowClosed;
        private Sprite glassOpen;
        private Sprite glassClosed;
        private Sprite switchOpen;
        private Sprite switchClosed;

        // State Management
        private bool magnifyingGlassInUse = false;
        private bool magnifyingGlassOpen = false;
        private int scanState = (int) ScanState.NONE;
        private int scanStateLength = Enum.GetNames(typeof(ScanState)).Length;
        
        private void Awake()
        {
            glassShadowOpen = FileLoader.GetSpriteByName(GLASS_SHADOW_OPEN);
            glassShadowClosed = FileLoader.GetSpriteByName(GLASS_SHADOW_CLOSED);
            glassOpen = FileLoader.GetSpriteByName(GLASS_OPEN);
            glassClosed = FileLoader.GetSpriteByName(GLASS_CLOSED);
            switchOpen = FileLoader.GetSpriteByName(GLASS_SWITCH_OPEN);
            switchClosed = FileLoader.GetSpriteByName(GLASS_SWITCH_CLOSED);
        }

        private void Update()
        {
            if (!glassShadowReference.GetMagnifyingGlassInUse()) return;
            ListenForMouseWheel();
        }

        private void OnMouseOver()
        {
            if (!glassShadowReference.GetMagnifyingGlassInUse()) return;
            if(MouseInput.LeftClick()) HandleLeftClick();
        }
        
        private void HandleLeftClick()
        {
            magnifyingGlassOpen = !magnifyingGlassOpen;
            if (magnifyingGlassOpen)
            {
                glassShadowSpriteRenderer.sprite = glassShadowOpen;
                glassMainSpriteRenderer.sprite = glassOpen;
                glassSwitchSpriteRenderer.sprite = switchOpen;
                scanState = (int) ScanState.SKIN;
                EnableMiddleLens();
            }
            else
            {
                glassShadowSpriteRenderer.sprite = glassShadowClosed;
                glassMainSpriteRenderer.sprite = glassClosed;
                glassSwitchSpriteRenderer.sprite = switchClosed;
                DisableAllLenses();
            }
        }
        
        private void ListenForMouseWheel()
        {
            if (!magnifyingGlassOpen)
            {
                scanState = (int) ScanState.NONE;
                return;
            }
            if (MouseInput.ScrollForward())
            {
                scanState++;
                UpdateLensByState();
            }
            else if (MouseInput.ScrollBackward())
            {
                scanState--;
                UpdateLensByState();
            }
        }

        private void UpdateLensByState()
        {
            if (scanState >= scanStateLength)
            {
                scanState = (int) ScanState.SKIN;
            }
            if (scanState <= 0)
            {
                scanState = (int) ScanState.BONES;
            }
            switch (scanState)
            {
                case (int)ScanState.NONE:
                    DisableAllLenses();
                    break;
                case (int)ScanState.SKIN:
                    DisableAllLenses();
                    EnableMiddleLens();
                    break;
                case (int)ScanState.ORGANS:
                    DisableAllLenses();
                    EnableLeftLens();
                    break;
                case (int)ScanState.BONES:
                    DisableAllLenses();
                    EnableRightLens();
                    break;
            }
        }

        private void DisableAllLenses()
        {
            MagnifyingGlass mg = magnifyingGlassReference.GetComponent<MagnifyingGlass>();
            mg.getMiddleLensReference().SetActive(false);
            mg.getLeftLensReference().SetActive(false);
            mg.getRightLensReference().SetActive(false);
        }

        private void EnableMiddleLens()
        {
            MagnifyingGlass mg = magnifyingGlassReference.GetComponent<MagnifyingGlass>();
            mg.getMiddleLensReference().SetActive(true);
        }

        private void EnableLeftLens()
        {
            MagnifyingGlass mg = magnifyingGlassReference.GetComponent<MagnifyingGlass>();
            mg.getLeftLensReference().SetActive(true);
        }
        
        private void EnableRightLens()
        {
            MagnifyingGlass mg = magnifyingGlassReference.GetComponent<MagnifyingGlass>();
            mg.getRightLensReference().SetActive(true);
        }
    }
}
