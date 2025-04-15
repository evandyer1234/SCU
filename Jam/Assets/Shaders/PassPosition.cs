using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPosition : MonoBehaviour
{
    private Material lensMaterial;
    private void Awake()
    {
        lensMaterial = GetComponent<MeshRenderer>().material;
    }
    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        lensMaterial.SetVector("_Offset", screenPos);
    }
}
