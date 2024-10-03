using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PointandClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //Transform objectHit = hit.transform;
                Debug.Log("Hit " + hit.collider.gameObject.name);
                ClickEvent ce = hit.collider.gameObject.GetComponent<ClickEvent>();
                ce.Clicked();
               
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {

        }
    }
}
