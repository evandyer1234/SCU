using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGear : MonoBehaviour
{
    [SerializeField] GameObject goodgear;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "trash")
        {
            goodgear.SetActive(true);
            Destroy(this.gameObject);
        }
    }
   

        
}
