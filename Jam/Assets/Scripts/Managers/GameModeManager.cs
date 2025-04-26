using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{

    [SerializeField] GameObject Gameover;
    [SerializeField] GameObject Gamewin;
    [SerializeField] float StartTime;
    [SerializeField, Tooltip("A reference to the post it displaying progress")] 
    GameObject postIt;
    
    public static GameModeManager instance;
    public float CurrentTime;
    
    // ingredients
    public List<Ingredient.ItemVal> neededItems = new List<Ingredient.ItemVal>();
    private List<string> collectedIngredientNames = new List<string>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CurrentTime = StartTime;
        Time.timeScale = 1f;
    }

    void FixedUpdate()
    {
        CurrentTime -= Time.fixedDeltaTime;
        UIManager.instance.SetTimerText("" + TimeSpan.FromSeconds(CurrentTime).Minutes.ToString("00") + " : " + TimeSpan.FromSeconds(CurrentTime).Seconds.ToString("00"));
        if (CurrentTime <= 0)
        {
            Time.timeScale = 0;
            Gameover.SetActive(true);
        }
        
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
    }

    public void AddIngredient(string ingredientName)
    {
        collectedIngredientNames.Add(ingredientName);

        string ingredientsToDisplay = "Ingredients:\n";
        foreach (var ingName in collectedIngredientNames)
        {
            ingredientsToDisplay += "\n* " + ingName;
        }
        postIt.GetComponent<TextMeshPro>().text = ingredientsToDisplay;
    }
    
    public void subtractTime(float amount)
    {
        CurrentTime -= amount;
    }

    public void Win()
    {
        Time.timeScale = 0;
        Gamewin.SetActive(true);
    }
}
