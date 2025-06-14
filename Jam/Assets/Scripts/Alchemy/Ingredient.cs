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
    public string GetIngredientName()
    {
        return name;
    }

    public void AddIngredientOperation(string operation)
    {
        this.ingredientOperations.Add(operation);
    }

    public List<string> GetIngredientOperations()
    {
        return ingredientOperations;
    }
    
    public Sprite ResolveSpriteByIngredientName()
    {
        if (IngredientConstants.PLANT_INGREDIENT_TO_SPRITE_LIST.Contains(name))
        {
            return FileLoader.GetSpriteByName("proto_plant");
        }

        if (IngredientConstants.LIQUID_INGREDIENT_TO_SPRITE_LIST.Contains(name))
        {
            return FileLoader.GetSpriteByName("proto_liquid");
        }

        if (IngredientConstants.MINERAL_INGREDIENT_TO_SPRITE_LIST.Contains(name))
        {
            return FileLoader.GetSpriteByName("proto_mineral");
        }
            
        Debug.LogWarning("FAILED TO MATCH INGREDIENT NAME TO SPRITE TYPE");
        return null;
    }
}
