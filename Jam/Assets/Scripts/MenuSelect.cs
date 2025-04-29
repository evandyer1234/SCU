using UnityEngine;

public class MenuSelect : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
    
    [SerializeField, Tooltip("The top level Start Menu to load initially")] 
    private GameObject startMenu;

    void Start()
    {
        //default state: only start menu enabled
        DisableAllMenus();
        startMenu.SetActive(true);
    }

    public void ChangeMenu(GameObject selectedmenu)
    {
        DisableAllMenus();
        selectedmenu.SetActive(true);
    }

    void DisableAllMenus()
    {
        for(int i = 0; i < _canvas.childCount; i++)
        {
            _canvas.GetChild(i).gameObject.SetActive(false);
        }
    }
}
