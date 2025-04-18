using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameModeManager : MonoBehaviour
{

    public static GameModeManager instance;
    
    [SerializeField] float StartTime;
    public float CurrentTime;
    
    public List<Ingredient.ItemVal> neededItems = new List<Ingredient.ItemVal>();
    [SerializeField] GameObject Gameover;
    [SerializeField] GameObject Gamewin;

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
