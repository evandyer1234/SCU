using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Subject", menuName = "ScriptableObjects/Subjects")]
public class Subject : ScriptableObject
{
    public List<MiniGameBase> miniGames;

    public string name;

    public Sprite imageMain;
    public Sprite imageOutLine;
    public Sprite imageSkeleton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
