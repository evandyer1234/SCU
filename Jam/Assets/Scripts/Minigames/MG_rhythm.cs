using System.Collections.Generic;
using UnityEngine;

public class MG_rhythm : MiniGameBase
{
    [Tooltip("Total notes the player needs to hit to complete the minigame")]
    [SerializeField] int totalnotes;

    [Tooltip("The amount off the ideal value the player can be to still register a hit")]
    [SerializeField] float variance;

    [Tooltip("The Ideal value the player is trying to hit")]
    [SerializeField] float hitnum;

    [Tooltip("The speed at which the notes move accross the screen")]
    [SerializeField] float notespeed;

    [Tooltip("The soonest a new note can spawn")]
    [SerializeField] float minspread;

    [Tooltip("The latest a new note can spawn")]
    [SerializeField] float maxspread;

    //probably should be a courutine, but its not a huge deal
    [Tooltip("current value of the spawn timer")]
    float currentspread;

    [Tooltip("Transformation parent for the spawned notes")]
    [SerializeField] Transform Parent;

    [Tooltip("Base note the script will spawn the notes from")]
    [SerializeField] RM_Note noteprefab;

    //list of notes in the scene to avoid searching the whole scene for the notes
    List<RM_Note> notes = new List<RM_Note>();

    public override void Start()
    {
        base.Start();
        //setup first note
        currentspread = Random.Range(minspread, maxspread);
    }

    public void FixedUpdate()
    {
        //lower note timer
        currentspread -= Time.deltaTime;

        //if at zero, reset timer and spawn new note
        if (currentspread <= 0)
        {
            currentspread = Random.Range(minspread, maxspread);

            RM_Note clone = Instantiate(noteprefab, Parent.transform.position, Parent.transform.rotation, Parent);
            
            clone.speed = notespeed;
            clone.rm = this;
            notes.Add(clone);
            
        }
    }

    //runs when the player hits the button, checks if there are any notes in the hit range, if not, its a penalty
    public void beatcheck()
    {
        bool hit = false;
        foreach (RM_Note n in notes)
        {
            if (n.s.value > hitnum - variance && n.s.value < hitnum + variance) 
            {
                hit = true;
                totalnotes--;
                notes.Remove(n);
                
                Destroy(n.gameObject);
                break;
            }
        }
        if (!hit)
        {
            miss();
        }
        if (totalnotes <= 0)
        {
            OnSuccess();
        }
    }

    //runs when the player hits the button at the incorrect time or lets a note slip past
    public void miss()
    {
        // gameModeManager.subtractTime(penalty);
    }
}
