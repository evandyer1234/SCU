using Helpers;
using Managers;
using UnityEngine;

namespace DefaultNamespace
{
    public class HelpInstructions : MonoBehaviour
    {

        [SerializeField] private GameObject minigameHelp;
        [SerializeField] private GameObject minigamePotionHelp;
        [SerializeField] private GameObject alchemyHelp;
        [SerializeField] private GameObject helpShadow;
        
        private PauseMenuManager _pauseMenuManager;
        private SubjectManager _subjectManager;

        private bool _isOpen = false;
        
        private void Awake()
        {
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
            
            DisableAllHelp();
            helpShadow.SetActive(false);
        }

        private void Start()
        {
            _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();
        }

        public void ToggleHelp()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            
            if (_isOpen)
            {
                DisableAllHelp();
                helpShadow.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                OpenSceneRespectiveHelp();
                helpShadow.SetActive(true);
                Time.timeScale = 0f;
            }
            
            _isOpen = !_isOpen;
        }

        public bool isHelpOpen()
        {
            return _isOpen;
        }

        private void OpenSceneRespectiveHelp()
        {
            DisableAllHelp();
            if (SCUSceneManager.IsMinigameScene())
            {
                if (_subjectManager.IsPotionMode())
                {
                    minigamePotionHelp.SetActive(true);
                }
                else
                {
                    minigameHelp.SetActive(true);
                }
            } else if (SCUSceneManager.IsAlchemyScene())
            {
                alchemyHelp.SetActive(true);
            }
        }

        private void DisableAllHelp()
        {
            minigameHelp.SetActive(false);
            minigamePotionHelp.SetActive(false);
            alchemyHelp.SetActive(false);
        }
    }
}