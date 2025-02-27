using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Gear : MiniGameBase
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject parts;
    [SerializeField] LineRenderer pullcord;
    [SerializeField] Transform handle;
    [SerializeField] Transform root;


    [HideInInspector] bool gearremoved = false;
    [HideInInspector] bool gearadded = false;


    public override void Update()
    {
        base.Update();
        Vector3[] pos = { handle.position, root.position };
        pullcord.SetPositions(pos);
    }
   

    public void OpenDoor()
    {
        door.SetActive(false);
        parts.SetActive(true);
    }

    public void CloseDoor()
    {
        door.SetActive(true);
        parts.SetActive(false);
    }
}
