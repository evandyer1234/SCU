using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class Ingredient
{
    private string name;
    private List<string> ingredientOperations = new();

    public Ingredient(string name)
    {
        this.name = name;
    }


    public Ingredient(string name, List<string> ingredientOperations)
    {
        this.name = name;
        this.ingredientOperations = ingredientOperations;
    }
    public string GetName()
    {
        return name;
    }

    public void AddIngredientOperation(string operation)
    {
        if (ingredientOperations.Contains(operation)) return;
        ingredientOperations.Add(operation);
    }

    public List<string> GetOperations()
    {
        return ingredientOperations;
    }
    
    public Sprite ResolveSpriteIconByIngredientName()
    {
        if (name.Equals(IngredientConstants.INGREDIENT_ID_CHIMERA_CLAW))
            return FileLoader.GetSpriteByName("chimera_claw_icon");
        
        if (name.Equals(IngredientConstants.INGREDIENT_ID_102_PURE_TEA))
            return FileLoader.GetSpriteByName("102_pure_tea_icon");
            
        if (name.Equals(IngredientConstants.INGREDIENT_ID_SUN_BATHED_IVY))
            return FileLoader.GetSpriteByName("sunbathed_ivy_icon");
        
        if (name.Equals(IngredientConstants.INGREDIENT_ID_MOON_MUSHROOM))
            return FileLoader.GetSpriteByName("moon_mushroom_icon");
        
        if (name.Equals(IngredientConstants.INGREDIENT_ID_MOONSTONE))
            return FileLoader.GetSpriteByName("moon_stone_icon");
        
        if (name.Equals(IngredientConstants.INGREDIENT_ID_AMARANTH))
            return FileLoader.GetSpriteByName("amaranth_icon");
        
        if (name.Equals(IngredientConstants.INGREDIENT_ID_SPIRIT_OF_THE_SAGES))
            return FileLoader.GetSpriteByName("spirit_of_the_sages_icon");
        
        if (name.Equals(IngredientConstants.INGREDIENT_ID_FAIRY_DUST))
            return FileLoader.GetSpriteByName("fairy_dust_icon");
        
        if (name.Equals(IngredientConstants.INGREDIENT_ID_MYRRH))
            return FileLoader.GetSpriteByName("myrrh_icon");
        
        if (name.Equals(IngredientConstants.INGREDIENT_ID_DEATHS_FLOWER))
            return FileLoader.GetSpriteByName("death_flower_icon");
        
        Debug.LogWarning("FAILED TO MATCH INGREDIENT NAME TO SPRITE TYPE");
        return null;
    }
}
