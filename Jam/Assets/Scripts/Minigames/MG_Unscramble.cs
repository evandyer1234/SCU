using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MG_Unscramble : MiniGameBase
{
    [Tooltip("List of words the game can generate")]
    [SerializeField] List<string> WordList = new List<string>();

    //list of letters on the board
    List<Letter> letters = new List<Letter>();

    [Tooltip("Canvas the game is on")]
    [SerializeField] GameObject Canvas;

    [Tooltip("centerpoint the letters will float to")]
    [SerializeField] Transform center;

    [Tooltip("letter prefab")]
    [SerializeField] Letter defaultletter;

    //word the game lands on
    string word;

    [Tooltip("bounds the letters can spawn in")]
    [SerializeField] SpriteRenderer spriteRenderer;
    bool done = false;

    public override void Start()
    {
        base.Start();

        //chooses a random word from the list
        word = WordList[Random.Range(0, WordList.Count)];
        Bounds b = spriteRenderer.bounds;
        //b.extents
        
        //places the letters on the board
        for (int i = 0; i < word.Length; i++)
        {
            float offsetX = Random.Range(-b.extents.x, b.extents.x);
            float offsetY = Random.Range(-b.extents.y, b.extents.y);
            Vector3 pos = b.center + new Vector3(offsetX, offsetY, 0f);
            Letter clone = Instantiate(defaultletter, pos, Quaternion.identity);
            clone.lettersetup(word[i]);
            clone.transform.SetParent(Canvas.transform);
            letters.Add(clone);
        }
        //starts checking if the game is done
       StartCoroutine(CheckString());
    }

    private void FixedUpdate()
    {
        //only plays once the game is finished
        if (done)
        {
            foreach (Letter l in letters) 
            {
                //moves all the letters so they align with the y coord of the center transform
                Vector3 v = l.gameObject.transform.position;
                l.gameObject.transform.position = Vector3.MoveTowards(v, new Vector3(v.x, center.position.y, v.z), 0.1f);
            }
        }
    }

    //checks if the game is done, just trust me
    IEnumerator CheckString()
    {
        yield return new WaitForSeconds(.5f);

        List<Letter> orderedletters = new List<Letter>();

        PQ<Letter> pq = new PQ<Letter>();

        foreach (Letter l in letters)
        {
            pq.Enqueue(l, l.transform.position.x + 600f);
        }

        bool b = true;
        int i = 0;

        for (LinkedListNode<PQ<Letter>.PQElement> node = pq.linkedList.First; node != null; node = node.Next)
        {
            Letter j = node.Value.data;
           
            if (j.letter != word[i])
            {
                b = false;
                break;
            }
            i++;
        }
        //if completed, starts the end stuff
        if (b)
        {
            StartCoroutine(Finish());
        }

        //if not, loops and checks again
        else
        {
            StartCoroutine(CheckString());
        }
    }
    //waits a couple seconds so the letters can align and the player knows the game is finished
    IEnumerator Finish()
    {
        done = true;
        yield return new WaitForSeconds(2f);
        OnSuccess();
    }
}
