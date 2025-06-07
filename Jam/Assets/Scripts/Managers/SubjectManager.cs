using System.Collections.Generic;
using System.Linq;
using Helpers;
using Managers;
using Minigames;
using Subjects;
using UnityEngine;

public class SubjectManager : MonoBehaviour
{
    [SerializeField] private GameObject corruptionMarkPrefab;
    
    public Subject currentSubject;

    // progression state
    private Dictionary<string, Subject> subjectsToCure = new();
    private Dictionary<string, Ingredient> allIngredients = new();
    private List<string> collectedIngredientHints = new();
    
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
    
    /** ****************************************************
     * *************** MINIGAME SPECIFIC *******************
     * *****************************************************
     */
    private bool _minigamesFullyLoaded = false;
    private bool _corruptionMarksPlaced = false;
    // needs to be a countdown instead of bool, since reloading the scene might trigger initiation in same frame
    private int _minigameSceneInitiationCountDown = 0;
    private PatientPage _patientPage;
    private MagnifyingGlass _magnifyingGlass;
    private MiniGameManager _miniGameManager;
    /* **************************************************** */
    
    /** ****************************************************
     * *************** ALCHEMY SPECIFIC ********************
     * *****************************************************
     */
    private AlchemyManager _alchemyManager;

    private bool alchemyShelvesFilled = false;
    
    /* **************************************************** */
    
    /** ****************************************************
     * **************** UNITY INTERFACE ********************
     * *****************************************************
     */
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        subjectsToCure = SubjectFactory.CreateSubjectMap();
        allIngredients = IngredientFactory.CreateIngredients();
    }

    private void Update()
    {
        LoadMinigamesAndSceneStateMachine();
        LoadAlchemyAndSceneStateMachine();
    }

    private void LoadAlchemyAndSceneStateMachine()
    {
        if (alchemyShelvesFilled) return;
        if (!IsAlchemySceneLoaded()) return;
        
        _alchemyManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_ALCHEMY_MANAGER)
            .GetComponent<AlchemyManager>();

        _alchemyManager.FillShelvesWithRespectiveIngredients(allIngredients.Values.ToList());
        alchemyShelvesFilled = true;
    }
    
    private bool IsAlchemySceneLoaded()
    {
        return GameObject.FindGameObjectWithTag(NamingConstants.TAG_ALCHEMY_MANAGER) != null;
    }

    /** ****************************************************
    * *************** GLOBAL PROGRESSION *******************
    * *****************************************************+
    */

    public void AddUniqueIngredient(string ingredientName)
    {
        if (!collectedIngredientHints.Contains(ingredientName))
        {
            collectedIngredientHints.Add(ingredientName);    
        }
    }
    
    /** ****************************************************
    * *************** MINIGAME SPECIFIC *******************
    * *****************************************************
    */
    
    public void LaunchMinigames(string subjectName)
    {
        _minigameSceneInitiationCountDown = 3;
        currentSubject = GetCurrentSubjectByName(subjectName);
        LoadMinigameManager();
        if (_miniGameManager != null)
        {
            _miniGameManager.InitializeTimer();
        }
    }

    public void ResetMinigameState()
    {
        currentSubject = null;
        _minigameSceneInitiationCountDown = 3;
        _minigamesFullyLoaded = false;
        _patientPage = null;
        _magnifyingGlass = null;
        _miniGameManager = null;
        _corruptionMarksPlaced = false;
    }

    public SCUSceneManager GetSCUSceneManager()
    {
        return GetComponent<SCUSceneManager>();
    }
    
    private void LoadMinigamesAndSceneStateMachine()
    {
        if (_minigamesFullyLoaded) return;
        if (currentSubject == null) return;
        if (!IsMinigameSceneInitationComplete()) return;
        if (!IsMinigameSceneLoaded()) return;
        
        if (!AreMinigamesFullyLoaded())
        {
            LoadPatientPage();
            LoadMinigameManager();
            LoadMagnifyingGlass();
            LoadCorruptionMarksAndMinigamesForCurrentSubject();
        }
        else
        {
            /* improve performance by checking a boolean instead of null checks each frame on gameobjects */
            _minigamesFullyLoaded = true;
        }
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

    private bool IsMinigameSceneInitationComplete()
    {
        if (_minigameSceneInitiationCountDown >= 0)
        {
            _minigameSceneInitiationCountDown--;
        }

        if (_minigameSceneInitiationCountDown > 0)
        {
            return false;
        }

        return true;
    }

    private bool IsMinigameSceneLoaded()
    {
        return GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINIGAME_MANAGER) != null;
    }
    
    /** ****************************************************
    * ************* END: MINIGAME SPECIFIC *****************
    * *****************************************************
    */
    
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
