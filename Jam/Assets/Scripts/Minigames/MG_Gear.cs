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
    [SerializeField] float pullstrength = 100f;
    [SerializeField] float pulldistance = 100f;
    float prevdis;

    public override void Start()
    {
        base.Start();
        CloseDoor();
    }
    public bool gearchanged = false; 
    public void Update()
    {
        Vector3[] pos = { handle.position, root.position };
        pullcord.SetPositions(pos);

        if (gearchanged)
        {
            float dis = Vector3.Distance(handle.position, root.position);
            
            if (dis > 0.2f)
            {
                //pullstrength -= dis * Time.fixedDeltaTime;
            }

            if (prevdis < dis)
            {
                prevdis = dis;
                pullstrength -= dis;
            }
            
            if (dis >= pulldistance)
            {

            }

            if (pullstrength <= 0)
            {
                OnSuccess();
            }
        }
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
