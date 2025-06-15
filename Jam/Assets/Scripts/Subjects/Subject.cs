using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    public string name;

    public Sprite imageOutfit;
    public Sprite imageUnder;
    public Sprite imageOrgans;
    public Sprite imageSkeleton;
    public bool isCured = false;
    public bool isAdult = false;

    public List<string> subjectMinigames = new();
    public List<Ingredient> neededIngredientsFromPotion = new();
    public List<string> ingredientHints = new();
    public List<string> allergies = new();
}
