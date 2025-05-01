using System.Collections.Generic;
using Helpers;
using Subjects;
using UnityEngine;

public class SubjectManager : MonoBehaviour
{
    [SerializeField] private GameObject corruptionMarkPrefab;
    
    private Subject currentSubject;

    // Subject progression state
    private Dictionary<string, Subject> subjectsToCure = new();

    private List<string> subjectCureOrder = new List<string>
    {
        SUBJECT_NAME_15,
        SUBJECT_NAME_14,
        SUBJECT_NAME_04,
    };
    
    // subject ids = names
    public const string SUBJECT_NAME_04 = "subject_04";
    public const string SUBJECT_NAME_14 = "subject_14";
    public const string SUBJECT_NAME_15 = "subject_15";
    
    private bool _minigamesInitiating = false;
    private bool _minigamesFullyLoaded = false;
    private bool _corruptionMarksPlaced = false;

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
        if (_minigamesFullyLoaded) return;
        if (!IsMinigameSceneLoaded()) return;
        
        if (_minigamesInitiating && !AreMinigamesFullyLoaded())
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

    private void LoadMinigameManager()
    {
        if (_miniGameManager != null) return;
        
        var _minigameManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINIGAME_MANAGER);
        if (_minigameManager != null){
            _miniGameManager = _minigameManager.GetComponent<MiniGameManager>();
        }
    }
    
    public void LaunchMinigames(string subjectName)
    {
        _minigamesInitiating = true;
        currentSubject = GetCurrentSubjectByName(subjectName);
    }

    private void LoadCorruptionMarksAndMinigamesForCurrentSubject()
    {
        if (currentSubject == null) return;
        if (_corruptionMarksPlaced) return;
        
        foreach (var subjectMinigame in currentSubject.subjectMinigames)
        {
            var corrMarkGO = Instantiate(
                corruptionMarkPrefab,
                CorruptionMarkPositions.GetPositionByMinigameId(subjectMinigame, currentSubject.isAdult),
                Quaternion.identity
            );
            corrMarkGO.transform.SetParent(GetSubjectScanLayerFromMinigameId(subjectMinigame).transform.parent, false);
            var cm = corrMarkGO.GetComponentInChildren<CorruptionMark>();
            cm.minigameRef = GetMinigameByMinigameId(subjectMinigame);
            cm.lensRef = GetMagnifyingGlassLensForMinigameId(subjectMinigame);
        }

        _corruptionMarksPlaced = true;
    }

    private GameObject GetMinigameByMinigameId(string minigameId)
    {
        foreach (var minigame in _miniGameManager.miniGames)
        {
            if (minigame.CompareTag(minigameId)) return minigame.gameObject;
        }

        Debug.LogWarning($"Failed to load matching minigame from MinigameManager by tag {minigameId}");
        return null;
    }

    private GameObject GetMagnifyingGlassLensForMinigameId(string minigameId)
    {
        switch (minigameId)
        {
            case NamingConstants.TAG_MINIGAME_HEARTSTRING:
                return _magnifyingGlass.leftLensReference;
            case NamingConstants.TAG_MINIGAME_LUNGPUMP:
                return _magnifyingGlass.leftLensReference;
            case NamingConstants.TAG_MINIGAME_DRAIN:
                return _magnifyingGlass.middleLensReference;
        }
        
        Debug.LogWarning($"Failed to match Magnifying Glass Lens for minigameId {minigameId}");
        return null;
    }
    
    private GameObject GetSubjectScanLayerFromMinigameId(string minigameId)
    {
        switch (minigameId)
        {
            case NamingConstants.TAG_MINIGAME_HEARTSTRING:
                return _patientPage.organLayer;
            case NamingConstants.TAG_MINIGAME_LUNGPUMP:
                return _patientPage.organLayer;
            case NamingConstants.TAG_MINIGAME_DRAIN:
                return _patientPage.underClothLayer;
        }

        Debug.LogWarning($"Failed to match Patient Scan Layer for minigameId {minigameId}");
        return null;
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
    
    private void LoadPatientPage()
    {
        if (_patientPage != null) return;
        
        var _patientPageGO = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PATIENT_PAGE);
        if (_patientPageGO != null){
            _patientPage = _patientPageGO.GetComponent<PatientPage>();
            _patientPage.SetSpriteBySubject(currentSubject);
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

        Debug.LogWarning("FAILED TO LOAD SUBJECT TO CURE!");
        return null;
    }
}
