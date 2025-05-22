using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class HoverTooltip : MonoBehaviour
{
    [SerializeField] private GameObject _tooltip;
    [SerializeField] private GameObject _canvasGroup;

    private void Start()
    {
        _tooltip.SetActive(false);
    }

    private void OnMouseEnter()
    {
        //Debug.Log("hover");
        _tooltip.SetActive(true);
    }

    private void OnMouseExit()
    {
        //Debug.Log("mouse exit");
        _tooltip.SetActive(false);
    }
}
