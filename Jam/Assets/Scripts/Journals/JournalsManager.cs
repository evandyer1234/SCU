using Helpers;
using UnityEngine;

namespace Journals
{
    public class JournalsManager : MonoBehaviour
    {
        [SerializeField] private GameObject patient15LeviJournal;
        [SerializeField] private GameObject patient14MiloJournal;
        [SerializeField] private GameObject patient04LaviniaJournal;

        private SubjectManager _subjectManager;
        
        void Start()
        {
            _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();

            SetAllJournalsInactive();
            
            if (_subjectManager.currentSubject != null)
            {
                var currSubject = _subjectManager.currentSubject;
                if (currSubject.name == SubjectManager.SUBJECT_NAME_04)
                {
                    patient04LaviniaJournal.SetActive(true);
                } else if (currSubject.name == SubjectManager.SUBJECT_NAME_15)
                {
                    patient15LeviJournal.SetActive(true);
                } else if (currSubject.name == SubjectManager.SUBJECT_NAME_14)
                {
                    patient14MiloJournal.SetActive(true);
                }
            }
        }

        public void ToMainMenu()
        {
            Time.timeScale = 1;
            var _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();
            _subjectManager.ResetMinigameState();
            _subjectManager.GetSCUSceneManager().TransitionToScene(NamingConstants.SCENE_ID_MAIN_MENU);
        }

        private void SetAllJournalsInactive()
        {
            patient15LeviJournal.SetActive(false);
            patient14MiloJournal.SetActive(false);
            patient04LaviniaJournal.SetActive(false);
        }
    }
}