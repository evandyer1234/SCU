using System.Collections.Generic;
using Minigames.Alchemy;
using UnityEngine;

namespace Managers
{
    public class AlchemyManager : MonoBehaviour
    {
        [SerializeField] private AlchemyShelf upperShelf;
        [SerializeField] private AlchemyShelf middleShelf;
        [SerializeField] private AlchemyShelf lowerShelf;

        public void FillShelvesWithRespectiveIngredients(List<Ingredient> ingredients)
        {
            upperShelf.FillShelfByType(ingredients);
            middleShelf.FillShelfByType(ingredients);
            lowerShelf.FillShelfByType(ingredients);
        }
    }
}