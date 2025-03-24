using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodGear : MonoBehaviour
{
    ClickandDrag cd;
    [SerializeField] MG_Gear mnigm;

    private void Start()
    {
        cd = GetComponent<ClickandDrag>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "NewGearSlot")
        {
            transform.position = other.transform.position;
            Destroy(cd);
            mnigm.gearchanged = true;
        }
    }
}
