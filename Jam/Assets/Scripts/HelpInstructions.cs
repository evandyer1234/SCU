using Helpers;
using Managers;
using UnityEngine;

namespace DefaultNamespace
{
    public class HelpInstructions : MonoBehaviour
    {

        [SerializeField] private GameObject minigameHelp;
        [SerializeField] private GameObject alchemyHelp;
        [SerializeField] private GameObject helpShadow;
        
        private PauseMenuManager _pauseMenuManager;

        private bool _isOpen = false;
        
        private void Awake()
        {
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
            
            minigameHelp.SetActive(false);
            alchemyHelp.SetActive(false);
            helpShadow.SetActive(false);
        }

        public void ToggleHelp()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            
            if (_isOpen)
            {
                minigameHelp.SetActive(false);
                alchemyHelp.SetActive(false);
                helpShadow.SetActive(false);
            }
            else
            {
                OpenSceneRespectiveHelp();
                helpShadow.SetActive(true);
            }
            
            _isOpen = !_isOpen;
        }

        private void OpenSceneRespectiveHelp()
        {
            if (SCUSceneManager.IsMinigameScene())
            {
                minigameHelp.SetActive(true);
                alchemyHelp.SetActive(false);
            } else if (SCUSceneManager.IsAlchemyScene())
            {
                minigameHelp.SetActive(false);
                alchemyHelp.SetActive(true);
            }
        }
    }
}