using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollow : MonoBehaviour
{

    public Transform pointATransform;
    public Transform pointBTransform;

    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //lineRenderer.SetPosition(0, pointATransform.position);
        lineRenderer.SetPosition(1, pointBTransform.localPosition);
    }
}
