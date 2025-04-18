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
        
        [SerializeField, Tooltip("The Under cloth Layer, also called Skin Layer")] 
        private GameObject underClothLayerReference;
        
        [SerializeField, Tooltip("The skeleton layer")] 
        private GameObject skeletonLayerReference;
        
        [SerializeField, Tooltip("The organ layer")] 
        private GameObject organLayerReference;
        
        // Addressable sprite names to match
        private static string GLASS_SHADOW_OPEN = "glass_shadow_open";
        private static string GLASS_SHADOW_CLOSED = "glass_shadow_closed";
        private static string GLASS_OPEN = "glass_open";
        private static string GLASS_CLOSED = "glass_closed";
        private static string GLASS_SWITCH_OPEN = "glass_switch_open";
        private static string GLASS_SWITCH_CLOSED = "glass_switch_closed";
        
        // Lens Colors
        private Color lensActive = new Color(0f, 0.9f, 0.75f, 0.16f);
        private Color lensInactive = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        
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
        private int scanState = (int) ScanMode.NONE;
        private int scanStateLength = Enum.GetNames(typeof(ScanMode)).Length;
        
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
                scanState = (int) ScanMode.SKIN;
                SetAllLensesActive(true);
                UnfocusAllLensesAndLayers();
                FocusMiddleLensAndLayer();
            }
            else
            {
                glassShadowSpriteRenderer.sprite = glassShadowClosed;
                glassMainSpriteRenderer.sprite = glassClosed;
                glassSwitchSpriteRenderer.sprite = switchClosed;
                UnfocusAllLensesAndLayers();
                SetAllLensesActive(false);
            }
        }
        
        private void ListenForMouseWheel()
        {
            if (!magnifyingGlassOpen)
            {
                scanState = (int) ScanMode.NONE;
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
                scanState = (int) ScanMode.SKIN;
            }
            if (scanState <= 0)
            {
                scanState = (int) ScanMode.BONES;
            }
            switch (scanState)
            {
                case (int)ScanMode.NONE:
                    UnfocusAllLensesAndLayers();
                    break;
                case (int)ScanMode.SKIN:
                    UnfocusAllLensesAndLayers();
                    FocusMiddleLensAndLayer();
                    break;
                case (int)ScanMode.ORGANS:
                    UnfocusAllLensesAndLayers();
                    FocusLeftLensAndLayer();
                    break;
                case (int)ScanMode.BONES:
                    UnfocusAllLensesAndLayers();
                    FocusRightLensAndLayer();
                    break;
            }
        }

        private void UnfocusAllLensesAndLayers()
        {
            MagnifyingGlass mg = magnifyingGlassReference.GetComponent<MagnifyingGlass>();
            RenderLensInactive(mg.getMiddleLensReference());
            RenderLensInactive(mg.getLeftLensReference());
            RenderLensInactive(mg.getRightLensReference());
            underClothLayerReference.SetActive(false);
            skeletonLayerReference.SetActive(false);
            organLayerReference.SetActive(false);
        }

        private void SetAllLensesActive(bool activeState)
        {
            MagnifyingGlass mg = magnifyingGlassReference.GetComponent<MagnifyingGlass>();
            mg.getMiddleLensReference().SetActive(activeState);
            mg.getLeftLensReference().SetActive(activeState);
            mg.getRightLensReference().SetActive(activeState);
        }

        private void FocusMiddleLensAndLayer()
        {
            MagnifyingGlass mg = magnifyingGlassReference.GetComponent<MagnifyingGlass>();
            RenderLensActive(mg.getMiddleLensReference());
            underClothLayerReference.SetActive(true);
        }

        private void FocusLeftLensAndLayer()
        {
            MagnifyingGlass mg = magnifyingGlassReference.GetComponent<MagnifyingGlass>();
            RenderLensActive(mg.getLeftLensReference());
            organLayerReference.SetActive(true);
        }
        
        private void FocusRightLensAndLayer()
        {
            MagnifyingGlass mg = magnifyingGlassReference.GetComponent<MagnifyingGlass>();
            RenderLensActive(mg.getRightLensReference());
            skeletonLayerReference.SetActive(true);
        }

        private void RenderLensActive(GameObject lensReference)
        {
            lensReference.GetComponent<SpriteRenderer>().color = lensActive;
            lensReference.GetComponent<SpriteMask>().enabled = true;
        }
        
        private void RenderLensInactive(GameObject lensReference)
        {
            lensReference.GetComponent<SpriteRenderer>().color = lensInactive;
            lensReference.GetComponent<SpriteMask>().enabled = false;
        }
    }
}
