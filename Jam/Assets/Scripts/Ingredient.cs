using Minigames;

public class Ingredient
{
    private string name;
    private IngredientType type;

    public Ingredient(string name, IngredientType type)
    {
        this.name = name;
        this.type = type;
    }
    
    public string GetIngredientName()
    {
        return name;
    }

    public IngredientType GetIngredientType()
    {
        return type;
    }
    
    
}
