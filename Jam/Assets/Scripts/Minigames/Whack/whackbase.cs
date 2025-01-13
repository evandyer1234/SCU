using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whackbase : MonoBehaviour
{
    public MG_Whack mw;
    public float uptime;
    public Transform slot;

    public void FixedUpdate()
    {
        uptime -= Time.deltaTime;

        if (uptime <= 0 )
        {
            Miss();
        }
    }

    //target gets hit gets hit
    public virtual void Hit()
    {
        
        OnBreak();
        Destroy(this.gameObject);
    }

    //target gets missed
    public virtual void Miss()
    {
        OnBreak();
        Destroy(this.gameObject);
    }

    //plays when a target leaves the board
    private void OnBreak()
    {
        mw.Spawnlocs.Add(slot);
    }

}
