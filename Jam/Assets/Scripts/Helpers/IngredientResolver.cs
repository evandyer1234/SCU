using System.Collections.Generic;

namespace Helpers
{
    public class IngredientResolver
    {
        public static List<Ingredient> IngredientsToList(
            List<string> collectedIngredients,
            Dictionary<string, Ingredient> allIngredients)
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            foreach (var currIngredientName in collectedIngredients)
            {
                ingredientList.Add(allIngredients[currIngredientName]);
            }
            return ingredientList;
        }
    }
}