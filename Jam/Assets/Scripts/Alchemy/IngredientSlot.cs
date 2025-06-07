using Helpers;
using Managers;
using UnityEngine;

namespace Minigames.Alchemy
{
    public class IngredientSlot : MonoBehaviour
    {
        [SerializeField] HoverTooltip hoverTooltip;

        private AlchemyManager _alchemyManager;
        private PauseMenuManager _pauseMenuManager;
        private SCUInputAction _scuInputAction;
        
        private Ingredient ingredient;

        void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            _alchemyManager = FindObjectOfType<AlchemyManager>();
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
        }

        private void OnMouseOver()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }
        }
        
        private void OnMouseEnter()
        {
            if (_pauseMenuManager.isGamePaused()) return;
        
            hoverTooltip.ShowTooltip();
        }

        private void OnMouseExit()
        {
            hoverTooltip.HideTooltip();
        }

        public void SetIngredient(Ingredient ingredient)
        {
            this.ingredient = ingredient;
            hoverTooltip.SetTooltipText(ingredient.GetIngredientName());
        }

        private void MouseLeftClick()
        {
            _alchemyManager.SetIngredientSelected(ingredient);
        }
    }
}