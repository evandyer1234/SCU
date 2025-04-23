using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour
{
    [SerializeField] private Transform _canvas;

    public void ChangeMenu(GameObject selectedmenu)
    {
        //Debug.Log(GameObject.Find("Canvas").transform.GetChild(0).name);

        for(int i = 0; i < _canvas.childCount; i++)
        {
            _canvas.GetChild(i).gameObject.SetActive(false);
        }

        selectedmenu.SetActive(true);
    }
    
    
}
