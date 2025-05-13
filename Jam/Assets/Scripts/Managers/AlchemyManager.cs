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

        void Awake()
        {
            /*
            List<Ingredient> ingredientsInShelf = new();
            ingredientsInShelf.Add(new Ingredient("abc", IngredientType.PLANT));
            ingredientsInShelf.Add(new Ingredient("abc", IngredientType.PLANT));
            ingredientsInShelf.Add(new Ingredient("abc", IngredientType.PLANT));
            ingredientsInShelf.Add(new Ingredient("abc", IngredientType.PLANT));
            ingredientsInShelf.Add(new Ingredient("abc", IngredientType.MINERAL));
            ingredientsInShelf.Add(new Ingredient("abc", IngredientType.MINERAL));
            ingredientsInShelf.Add(new Ingredient("abc", IngredientType.MINERAL));
            ingredientsInShelf.Add(new Ingredient("abc", IngredientType.LIQUID));
            ingredientsInShelf.Add(new Ingredient("abc", IngredientType.LIQUID));
            FillShelvesWithRespectiveIngredients(ingredientsInShelf);
            */
        }

        public void FillShelvesWithRespectiveIngredients(List<Ingredient> ingredients)
        {
            upperShelf.FillShelfByType(ingredients);
            middleShelf.FillShelfByType(ingredients);
            lowerShelf.FillShelfByType(ingredients);
        }
    }
}