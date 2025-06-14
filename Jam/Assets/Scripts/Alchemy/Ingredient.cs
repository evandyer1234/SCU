using System.Collections.Generic;
using Minigames;

public class Ingredient
{
    private string name;
    private IngredientType type;
    private List<string> ingredientOperations = new();

    public Ingredient(string name, IngredientType type)
    {
        this.name = name;
        this.type = type;
    }


    public Ingredient(string name, IngredientType type, List<string> ingredientOperations)
    {
        this.name = name;
        this.type = type;
        this.ingredientOperations = ingredientOperations;
    }
    public string GetIngredientName()
    {
        return name;
    }

    public IngredientType GetIngredientType()
    {
        return type;
    }

    public void AddIngredientOperation(string operation)
    {
        this.ingredientOperations.Add(operation);
    }

    public List<string> GetIngredientOperations()
    {
        return ingredientOperations;
    }
}
