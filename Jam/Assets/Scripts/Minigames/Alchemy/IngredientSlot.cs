using UnityEngine;

namespace Minigames.Alchemy
{
    public class IngredientSlot : MonoBehaviour
    {
        [SerializeField] HoverTooltip hoverTooltip;

        private Ingredient ingredient;

        public void SetIngredient(Ingredient ingredient)
        {
            this.ingredient = ingredient;
            hoverTooltip.SetTooltipText(ingredient.GetIngredientName());
        }
    }
}