using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MG_Unscramble : MiniGameBase
{
    [SerializeField] List<string> WordList = new List<string>();
    [SerializeField] List<Slot> SlotList = new List<Slot>();
    [SerializeField] GameObject Canvas;
    [SerializeField] Letter defaultletter;
    string word;
    [SerializeField] SpriteRenderer spriteRenderer;
    public override void Start()
    {
        word = WordList[Random.Range(0, WordList.Count)];
        Bounds b = spriteRenderer.bounds;
        //b.extents
        
        for (int i = 0; i < word.Length; i++)
        {
            float offsetX = Random.Range(-b.extents.x, b.extents.x);
            float offsetY = Random.Range(-b.extents.y, b.extents.y);
            Vector3 pos = b.center + new Vector3(offsetX, offsetY, 0f);
            Letter clone = Instantiate(defaultletter, pos, Quaternion.identity);
            clone.lettersetup(word[i]);
            clone.transform.SetParent(Canvas.transform);
        }
    }
}
