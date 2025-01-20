using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class leech : MonoBehaviour
{
    [SerializeField] MG_Drain _Drain;

    //when the leeche enters a trigger, it adds a leech to the leech counter in the main script
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _Drain.leeches++;
        }
    }
    //remeoves a leech when exiting the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _Drain.leeches--;
        }

    }


}
