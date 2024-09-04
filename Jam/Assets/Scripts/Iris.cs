using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris : MonoBehaviour
{

    public bool isIntactEye;
    public bool otherEyeExtracted;
    public bool extractionInProgress;
    public float timeBetweenMovements;
    public float irisMoveSpeed;

    public bool isCentered;

    private float timer = 0;
    private int randomInt;

    [SerializeField] private List<Vector3> eyePositions;

    private Vector3 originalPosition;
    
    public float jitterAmount = 0.1f;
    public float jitterSpeed = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
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

        if (isIntactEye && extractionInProgress)
        {
            Vector3 jitter = originalPosition + (Vector3)Random.insideUnitCircle * jitterAmount;
            transform.localPosition = jitter;
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
