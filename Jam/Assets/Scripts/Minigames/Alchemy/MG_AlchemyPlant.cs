using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_AlchemyPlant : MG_AlchemyBase
{
    public GameObject leafObject;
    public int leafNum = 5;
    public float leafDistance = 0.75f;
    public float leftX = 1.65f;
    public float rightX = 3.39f;
    public float maxHeight = 2.0f;
    public float minHeight = -3.0f;
    public float leafPickDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (enabled) PopulateLeaves();
    }

    void PopulateLeaves()
    {
        int leftLeaves = leafNum / 2;
        int rightLeaves = leafNum - leftLeaves;

        List<float> leftPoints = new List<float>();
        for (int i = 0; i < leftLeaves; i++)
        {
            bool breakLoop = false;
            while (!breakLoop)
            {
                float point = Random.Range(minHeight, maxHeight);
                foreach (float item in leftPoints)  
                {
                    if ((item - 1.0f) <= point && point <= (item + 1.0f))
                    {
                        break;
                    }
                }
                leftPoints.Add(point);
                breakLoop = true;
            }
        }

        List<float> rightPoints = new List<float>();
        for (int i = 0; i < rightLeaves; i++)
        {
            bool breakLoop = false; 
            while (!breakLoop)
            {
                float point = Random.Range(minHeight, maxHeight);
                breakLoop = true;
                foreach (float item in rightPoints)
                {
                    if ((item - 1.0f) <= point && point <= (item + 1.0f))
                    {
                        breakLoop = false;
                        break;
                    }
                }
                if (breakLoop)
                {
                    rightPoints.Add(point);
                }
            }
        }
        Debug.Log("Left");
        foreach (float point in leftPoints)
        {
            GameObject leaf = Instantiate(leafObject, new Vector3(leftX, point, -2.9f), Quaternion.identity);
            leaf.transform.parent = this.transform;
            leaf.GetComponent<MG_AlchemyLeaf>().pickDistance = leafPickDistance;
            //Debug.Log(point);
        }
        Debug.Log("Right");
        foreach (float point in rightPoints)
        {
            GameObject leaf = Instantiate(leafObject, new Vector3(rightX, point, -2.9f), Quaternion.identity);
            leaf.GetComponent<SpriteRenderer>().flipX = true;
            leaf.GetComponent<MG_AlchemyLeaf>().pickDistance = leafPickDistance;
            //Debug.Log(point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            Debug.Log("You win!");
            gameFinished = true;
            enabled = false;
        }
    }
}
