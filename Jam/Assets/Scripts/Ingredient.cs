using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : DragReset
{
    public enum ItemVal { HI, HowAreYou, Etc };

    public ItemVal value;
    bool inslot = false;

    public override void Release()
    {
        
        if (inslot)
        {
            FindObjectOfType<Pot>().drops.Add(value);
            Debug.Log("Yo");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Pot p = other.GetComponent<Pot>();
        if (p != null)
        {
            inslot = true;
            Debug.Log("In");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Pot p = other.GetComponent<Pot>();
        if (p != null)
        {
            inslot = false;
            Debug.Log("Out");
        }
    }
}
