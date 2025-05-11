using Helpers;
using UnityEngine;

namespace Managers
{
    public class GlobalDependencyManager : MonoBehaviour
    {
        [SerializeField] private GameObject eventSystemPrefab;
        [SerializeField] private GameObject customCursorPrefab;
        
        private void Awake()
        {
            LoadSubjectManagerSafely();
            LoadCustomCursorSafely();
        }
        
        private void LoadCustomCursorSafely()
        {
            var _cursorGameObject = GameObject.FindGameObjectWithTag(NamingConstants.TAG_CUSTOM_CURSOR);
            if (_cursorGameObject == null)
            {
                var _cursor = Instantiate(customCursorPrefab);
                
                _cursor.transform.Find("Glove").GetComponent<SpriteRenderer>().sortingOrder = 9999;
            }
        }
    
        private void LoadSubjectManagerSafely()
        {
            var _eventSystem = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM);
            if (_eventSystem == null)
            {
                Instantiate(eventSystemPrefab);
            }
        }
    }
}