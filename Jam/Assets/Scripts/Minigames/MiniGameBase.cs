using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameBase : MonoBehaviour
{
    public GameObject piece;
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnSuccess()
    {
        piece.SetActive(true);
        gameObject.SetActive(false);
    }
}
