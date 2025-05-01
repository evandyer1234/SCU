using System.Collections.Generic;
using Helpers;
using Managers;
using Subjects;
using UnityEngine;

public class SubjectManager : MonoBehaviour
{
    [SerializeField] private GameObject corruptionMarkPrefab;
    
    public Subject currentSubject;

    // Subject progression state
    private Dictionary<string, Subject> subjectsToCure = new();

    // subject ids = names
    public const string SUBJECT_NAME_04 = "subject_04";
    public const string SUBJECT_NAME_14 = "subject_14";
    public const string SUBJECT_NAME_15 = "subject_15";
    
    private List<string> subjectCureOrder = new List<string>
    {
        SUBJECT_NAME_15,
        SUBJECT_NAME_14,
        SUBJECT_NAME_04,
    };
    
    private bool _minigamesFullyLoaded = false;
    private bool _corruptionMarksPlaced = false;
    /*
     * this needs to be a countdown because a scene reload while beeing in the same scene
     * would instantly trigger initiation 
     */
    private int _minigamesInitiatingCountDown = 0;
    private int _countdownBase = 3;

    private PatientPage _patientPage;
    private MagnifyingGlass _magnifyingGlass;
    private MiniGameManager _miniGameManager;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        subjectsToCure = SubjectFactory.CreateSubjectMap();
    }

    private void Update()
    {
        if (_minigamesInitiatingCountDown >= 0)
        {
            _minigamesInitiatingCountDown--;
        }
        if (_minigamesInitiatingCountDown > 0) return;
        if (_minigamesFullyLoaded) return;
        if (!IsMinigameSceneLoaded()) return;
        
        if (!AreMinigamesFullyLoaded())
        {
            LoadPatientPage();
            LoadMinigameManager();
            LoadMagnifyingGlass();
            LoadCorruptionMarksAndMinigamesForCurrentSubject();
        }

        /* improve performance by checking a boolean instead of null checks on gameobjects */
        if (AreMinigamesFullyLoaded())
        {
            _minigamesFullyLoaded = true;
        }
    }
    
    public void LaunchMinigames(string subjectName)
    {
        _minigamesInitiatingCountDown = _countdownBase;
        currentSubject = GetCurrentSubjectByName(subjectName);
    }

    public void ResetMinigameState()
    {
        currentSubject = null;
        _minigamesInitiatingCountDown = _countdownBase;
        _minigamesFullyLoaded = false;
        _patientPage = null;
        _magnifyingGlass = null;
        _miniGameManager = null;
        _corruptionMarksPlaced = false;
    }

    private void LoadPatientPage()
    {
        if (_patientPage != null) return;
        
        var _patientPageGO = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PATIENT_PAGE);
        if (_patientPageGO != null){
            _patientPage = _patientPageGO.GetComponent<PatientPage>();
            _patientPage.SetSpriteBySubject(currentSubject);
        }
    }
    
    private void LoadMinigameManager()
    {
        if (_miniGameManager != null) return;
        
        var _minigameManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINIGAME_MANAGER);
        if (_minigameManager != null){
            _miniGameManager = _minigameManager.GetComponent<MiniGameManager>();
        }
    }
    
    private void LoadMagnifyingGlass()
    {
        if (_magnifyingGlass != null) return;
        
        var _magnifyingGlassGO = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAGNIFYING_GLASS);
        if (_magnifyingGlassGO != null)
        {
            _magnifyingGlass = _magnifyingGlassGO.GetComponent<MagnifyingGlass>();
        }
    }
    
    private void LoadCorruptionMarksAndMinigamesForCurrentSubject()
    {
        if (currentSubject == null) return;
        if (_corruptionMarksPlaced) return;
        
        foreach (var subjectMinigameId in currentSubject.subjectMinigames)
        {
            var corrMarkGO = Instantiate(
                corruptionMarkPrefab,
                CorruptionMarkPositions.GetPositionByMinigameId(subjectMinigameId, currentSubject.isAdult),
                Quaternion.identity
            );
            var parentGO = SubjectManagerMinigameResolver
                .GetSubjectScanLayerFromMinigameId(subjectMinigameId, _patientPage);
            corrMarkGO.transform.SetParent(parentGO.transform.parent, false);
            var cm = corrMarkGO.GetComponentInChildren<CorruptionMark>();
            cm.minigameRef = SubjectManagerMinigameResolver
                .GetMinigameByMinigameId(subjectMinigameId, _miniGameManager.miniGames);
            cm.lensRef = SubjectManagerMinigameResolver
                .GetMagnifyingGlassLensForMinigameId(subjectMinigameId, _magnifyingGlass);
        }

        _corruptionMarksPlaced = true;
    }

    private bool AreMinigamesFullyLoaded()
    {
        return _patientPage != null
               && _magnifyingGlass != null;
    }

    private bool IsMinigameSceneLoaded()
    {
        return GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINIGAME_MANAGER) != null;
    }
    
    private Subject GetCurrentSubjectByName(string name)
    {
        return subjectsToCure[name];
    }
    
    /* IN CASE WE WANT TO DEFINE A CURE ORDER FOR GAME PROGRESSION */
    /* Currently unused */
    private Subject GetCurrentSubjectByCureOrder()
    {
        foreach (var subjectKey in subjectCureOrder)
        {
            var subject = subjectsToCure[subjectKey];
            if (subject != null && !subject.isCured)
            {
                return subject;
            }
        }

        Debug.LogWarning("Failed to load Subject to Cure!");
        return null;
    }
}
