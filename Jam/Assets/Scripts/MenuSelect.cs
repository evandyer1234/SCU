using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour
{
    [SerializeField] List<GameObject> menus = new List<GameObject>();

    public void ChangeMenu(GameObject selectedmenu)
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        selectedmenu.SetActive(true);
    }
    
    
}
