using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MG_Whack : MiniGameBase
{
    [Tooltip("Spawn locations for the targets")]
    public List<Transform> Spawnlocs = new List<Transform>();

    [Tooltip("List of possible targets to spawn")]
    [SerializeField] List<whackbase> whackables = new List<whackbase>();

    [Tooltip("Canavs to correctly spawn targets")]
    [SerializeField] Transform Canvas;

    [Tooltip("Amount of hits needed to clear the game")]
    [SerializeField] int totalhits;

    [Tooltip("Rate at which the enemies will spawn")]
    [SerializeField] float spawnrate;

    [Tooltip("variance in the spawn time to keep it random")]
    [SerializeField] float spawnvariance;

    [Tooltip("How long the enemies stay up for")]
    [SerializeField] float uptime;

    [Tooltip("set to 0 for just moles, 1 for moles and civs")]
    [SerializeField] int difficulty;

    //to stop courutines from reseting when they arent supposed to
    bool done = false;
    
    
    // Audio
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _missSound;

    public override void Start()
    {
        base.Start();
        
        //start timer
        StartCoroutine(timer());
        
        _audioSource = GetComponent<AudioSource>();

    }
    IEnumerator timer()
    {
        
        yield return new WaitForSeconds(spawnrate);

        //if not done, restart the timer
        if (!done)
        {
            StartCoroutine(spawnmole(Random.Range(0, spawnvariance)));
            StartCoroutine(timer());
        }
    }

    //missnamed, decides which enemy to spawn after a random amount of time
    IEnumerator spawnmole(float secs)
    {
        yield return new WaitForSeconds(secs);
        if (Spawnlocs.Count > 0 )
        {
            int space = Random.Range(0, Spawnlocs.Count);

            if (difficulty == 0)
            {
                spawntarget(space, 0);
            }
            else if (difficulty == 1)
            {
                spawntarget(space, Random.Range(0, 2));
            }
           
        }
    }

    //spawns target based on the inputs from spawnmole. and sets them up too
    void spawntarget(int index, int itemtospawn)
    {
        whackbase clone = Instantiate(whackables[itemtospawn], Spawnlocs[index].transform.position, Spawnlocs[index].transform.rotation, Canvas);
        clone.mw = this;
        clone.uptime = uptime;
        clone.slot = Spawnlocs[index];
        Spawnlocs.RemoveAt(index);
    }

    //plays when the player misses a target, either by letting a mole get away or hitting a civ
    public void Miss()
    {
        // gameModeManager.subtractTime(penalty);
        Debug.Log("Miss");

        _audioSource.PlayOneShot(_missSound);
    }

    //plays wheh the player hits a mole
    public void Smack()
    {
        totalhits--;
        if (totalhits <= 0)
        {
            done = true;
            OnSuccess();
        }

        Debug.Log("Hit");
        
        _audioSource.PlayOneShot(_hitSound);

    }
}
