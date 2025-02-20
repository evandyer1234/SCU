using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyBellowsMG : MonoBehaviour
{
    GameObject fireSprite;
    GameObject beakerSprite;

    float fireShrinkRate = 1f;
    float fireGrowRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            if (Input.GetKeyDown("space"))
            {
                fireSprite.transform.localScale += new Vector3(fireGrowRate, fireGrowRate);
            }
            fireSprite.transform.localScale -= new Vector3(fireShrinkRate * Time.deltaTime, fireShrinkRate * Time.deltaTime);
        }
    }
}
