using System.Collections.Generic;
using Helpers;

namespace Minigames
{
    public class IngredientFactory
    {
        public static Dictionary<string, Ingredient> CreateIngredients()
        {
            Dictionary<string, Ingredient> ing = new Dictionary<string, Ingredient>();

            // PLANTS
            ing.Add(IngredientConstants.INGREDIENT_ID_NETTLE, new Ingredient(IngredientConstants.INGREDIENT_ID_NETTLE, IngredientType.PLANT));
            ing.Add(IngredientConstants.INGREDIENT_ID_SUN_BATHED_IVY, new Ingredient(IngredientConstants.INGREDIENT_ID_SUN_BATHED_IVY, IngredientType.PLANT));
            ing.Add(IngredientConstants.INGREDIENT_ID_DEATHS_FLOWER, new Ingredient(IngredientConstants.INGREDIENT_ID_DEATHS_FLOWER, IngredientType.PLANT));
            ing.Add(IngredientConstants.INGREDIENT_ID_SUSPENDED_ROSE, new Ingredient(IngredientConstants.INGREDIENT_ID_SUSPENDED_ROSE, IngredientType.PLANT));
            
            // LIQUIDS
            ing.Add(IngredientConstants.INGREDIENT_ID_MOONWATER, new Ingredient(IngredientConstants.INGREDIENT_ID_MOONWATER, IngredientType.LIQUID));
            ing.Add(IngredientConstants.INGREDIENT_ID_DARKENED_WATER, new Ingredient(IngredientConstants.INGREDIENT_ID_DARKENED_WATER, IngredientType.LIQUID));
            ing.Add(IngredientConstants.INGREDIENT_ID_DRAGONS_BLOOD, new Ingredient(IngredientConstants.INGREDIENT_ID_DRAGONS_BLOOD, IngredientType.LIQUID));
            ing.Add(IngredientConstants.INGREDIENT_ID_SWEET_VITRIOL, new Ingredient(IngredientConstants.INGREDIENT_ID_SWEET_VITRIOL, IngredientType.LIQUID));
            
            // MINERALS
            ing.Add(IngredientConstants.INGREDIENT_ID_SUNSTONE, new Ingredient(IngredientConstants.INGREDIENT_ID_SUNSTONE, IngredientType.MINERAL));
            ing.Add(IngredientConstants.INGREDIENT_ID_PEARL_ASH, new Ingredient(IngredientConstants.INGREDIENT_ID_PEARL_ASH, IngredientType.MINERAL));
            ing.Add(IngredientConstants.INGREDIENT_ID_LIME, new Ingredient(IngredientConstants.INGREDIENT_ID_LIME, IngredientType.MINERAL));
            ing.Add(IngredientConstants.INGREDIENT_ID_BRIMSTONE, new Ingredient(IngredientConstants.INGREDIENT_ID_BRIMSTONE, IngredientType.MINERAL));
            
            return ing;
        }
    }
}