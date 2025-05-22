using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class HoverTooltip : MonoBehaviour
{   
    [TextArea(5,20)]
    [SerializeField] private string _textToDisplay;

    [SerializeField] private GameObject _tooltip;
    [SerializeField] private TMP_Text _TMP;
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
