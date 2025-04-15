using UnityEngine;

namespace Subjects
{
    public class MagnifyingGlass : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        
        private bool followMouse = false;
        private Vector3 offsetSpriteToMouse = Vector3.zero;
        
        private void Update()
        {
            if (followMouse)
            {
                Vector3 mousepos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousepos.x - offsetSpriteToMouse.x, mousepos.y - offsetSpriteToMouse.y, transform.position.z);
            }
        }

        void OnMouseDown()
        {
            Debug.Log("OnMouseDown");
            followMouse = true;
            Vector3 mousepos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offsetSpriteToMouse = (mousepos - transform.position);
        }

        void OnMouseUp()
        {
            Debug.Log("OnMouseUp");
            followMouse = false;
        }
    }
}