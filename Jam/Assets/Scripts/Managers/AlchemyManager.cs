using System.Collections.Generic;
using Minigames;
using Minigames.Alchemy;
using UnityEngine;

namespace Managers
{
    public class AlchemyManager : MonoBehaviour
    {
        [SerializeField] private AlchemyShelf upperShelf;
        [SerializeField] private AlchemyShelf middleShelf;
        [SerializeField] private AlchemyShelf lowerShelf;
        [SerializeField] private DraggableIngredient _draggablePlantIngredient;
        [SerializeField] private DraggableIngredient _draggableLiquidIngredient;
        [SerializeField] private DraggableIngredient _draggableMineralIngredient;
        
        public void FillShelvesWithRespectiveIngredients(List<Ingredient> ingredients)
        {
            upperShelf.FillShelfByType(ingredients);
            middleShelf.FillShelfByType(ingredients);
            lowerShelf.FillShelfByType(ingredients);
        }

        public void SetIngredientSelected(Ingredient ingredient)
        {
            switch (ingredient.GetIngredientType())
            {
                case IngredientType.PLANT:
                    _draggablePlantIngredient.SetDraggableIngredient(ingredient);
                    break;
                case IngredientType.LIQUID:
                    _draggableLiquidIngredient.SetDraggableIngredient(ingredient);
                    break;
                case IngredientType.MINERAL:
                    _draggableMineralIngredient.SetDraggableIngredient(ingredient);
                    break;
                default:
                    Debug.LogWarning("FAILED TO MATCH SELECTED INGREDIENT TYPE");
                    break;
            }
        }
    }
}