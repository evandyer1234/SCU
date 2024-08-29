using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris : MonoBehaviour
{

    public bool isIntactEye;
    public bool otherEyeExtracted;
    public float timeBetweenMovements;
    public float irisMoveSpeed;

    public bool isCentered;

    private float timer = 0;
    private int randomInt;

    [SerializeField] private List<Vector3> eyePositions;
    
    // Start is called before the first frame update
    void Start()
    {
        randomInt = Random.Range(1, eyePositions.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (randomInt == 0)
        {
            isCentered = true;
        }
        else
        {
            isCentered = false;
        }
        
        if (isIntactEye && otherEyeExtracted)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, eyePositions[randomInt], irisMoveSpeed * Time.deltaTime);

        }
        
        if (timer <= 0)
        {
            int _randInt;
            _randInt = Random.Range(0, eyePositions.Count);

            if (_randInt == randomInt)
            {
                _randInt = Random.Range(0, eyePositions.Count);
            }
            else
            {
                randomInt = _randInt;
            }
            
            

            
            timer = timeBetweenMovements;
        }

        timer -= Time.deltaTime;
    }
}
