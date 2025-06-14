using Helpers;
using Managers;
using UnityEngine;

namespace Minigames.Alchemy
{
    public class IngredientSlot : MonoBehaviour
    {
        [SerializeField] string ingredientName;
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

            if (ingredientName == null || ingredientName == "")
            {
                Debug.LogError("FAILED TO IDENTIFY INGREDIENT NAME! PLEASE PROVIDE A MATCHING NAME FROM INGREDIENT CONSTANTS!");
                return;
            }
            ingredient = new Ingredient(ingredientName);
            if (ingredient.ResolveSpriteByIngredientName() == null)
            {
                Debug.LogError("FAILED TO IDENTIFY INGREDIENT SPRITE BY NAME! DO YOU HAVE A TYPO IN YOUR INGREDIENT?");
                return;
            }
            hoverTooltip.SetTooltipText(ingredient.GetIngredientName());
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

        private void MouseLeftClick()
        {
            _alchemyManager.SetIngredientSelected(ingredient);
        }
    }
}