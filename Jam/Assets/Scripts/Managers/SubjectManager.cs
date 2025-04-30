using System.Collections.Generic;
using Helpers;
using Subjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubjectManager : MonoBehaviour
{
    public static SubjectManager instance;
    
    private Subject currentSubject;

    // Subject progression state
    private Dictionary<string, Subject> subjectsCured = new();

    private List<string> subjectOrder = new List<string>
    {
        SUBJECT_NAME_15,
        SUBJECT_NAME_14,
        SUBJECT_NAME_04,
    };
    
    // subject ids = names
    public const string SUBJECT_NAME_04 = "subject_04";
    public const string SUBJECT_NAME_14 = "subject_14";
    public const string SUBJECT_NAME_15 = "subject_15";
    
    // subject sprite addressables
    private const string SUBJECT_SPRITENAME_OUTFIT_04 = "char_04_outfit";
    private const string SUBJECT_SPRITENAME_UNDER_04 = "char_04_under";
    private const string SUBJECT_SPRITENAME_ORGANS_04 = "char_04_organs";
    private const string SUBJECT_SPRITENAME_SKELETON_04 = "char_04_skeleton";
    private const string SUBJECT_SPRITENAME_OUTFIT_14 = "char_14_outfit";
    private const string SUBJECT_SPRITENAME_UNDER_14 = "char_14_under";
    private const string SUBJECT_SPRITENAME_ORGANS_14 = "char_14_organs";
    private const string SUBJECT_SPRITENAME_SKELETON_14 = "char_14_skeleton";
    private const string SUBJECT_SPRITENAME_OUTFIT_15 = "char_15_outfit";
    private const string SUBJECT_SPRITENAME_UNDER_15 = "char_15_under";
    private const string SUBJECT_SPRITENAME_ORGANS_15 = "char_15_organs";
    private const string SUBJECT_SPRITENAME_SKELETON_15 = "char_15_skeleton";

    private bool _minigamesLaunched = false;
    private bool _spritesLoadedForMinigames = false;

    private PatientPage _patientPage;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        subjectsCured = CreateSubjectMap();

        currentSubject = GetCurrentSubjectByCureOrder();
    }

    private void Update()
    {
        if (_minigamesLaunched && !_spritesLoadedForMinigames)
        {
            var _patientPageGO = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PATIENT_PAGE);
            if (_patientPageGO != null){
                _patientPage = _patientPageGO.GetComponent<PatientPage>();
                _patientPage.SetSpriteBySubject(currentSubject);
                _spritesLoadedForMinigames = true;
            }
        }
    }

    public void SetMinigamesLaunched(bool launched, string subjectName)
    {
        _minigamesLaunched = launched;
        _spritesLoadedForMinigames = false;
        currentSubject = GetCurrentSubjectByName(subjectName);
    }
    
    private static Subject CreateSubject(
        string name, 
        string outfit, 
        string under, 
        string organs, 
        string skeleton, 
        bool isAdult)
    {
        Subject subject = new Subject();
        subject.name = name;
        subject.imageOutfit = FileLoader.GetSpriteByName(outfit);
        subject.imageUnder = FileLoader.GetSpriteByName(under);
        subject.imageOrgans = FileLoader.GetSpriteByName(organs);
        subject.imageSkeleton = FileLoader.GetSpriteByName(skeleton);
        subject.isAdult = isAdult;
        return subject;
    }


    private Subject GetCurrentSubjectByName(string name)
    {
        return subjectsCured[name];
    }
    private Subject GetCurrentSubjectByCureOrder()
    {
        foreach (var subjectKey in subjectOrder)
        {
            var subject = subjectsCured[subjectKey];
            if (subject != null && !subject.isCured)
            {
                return subject;
            }
        }

        Debug.LogWarning("FAILED TO LOAD SUBJECT TO CURE!");
        return null;
    }
    
    private Dictionary<string, Subject> CreateSubjectMap()
    {
        Dictionary<string, Subject> subjectMap = new();
        Subject subject04 = CreateSubject(
            SUBJECT_NAME_04, 
            SUBJECT_SPRITENAME_OUTFIT_04, 
            SUBJECT_SPRITENAME_UNDER_04, 
            SUBJECT_SPRITENAME_ORGANS_04, 
            SUBJECT_SPRITENAME_SKELETON_04, 
            true);
        Subject subject14 = CreateSubject(
            SUBJECT_NAME_14, 
            SUBJECT_SPRITENAME_OUTFIT_14, 
            SUBJECT_SPRITENAME_UNDER_14,
            SUBJECT_SPRITENAME_ORGANS_14,
            SUBJECT_SPRITENAME_SKELETON_14,
            false);
        Subject subject15 = CreateSubject(
            SUBJECT_NAME_15, 
            SUBJECT_SPRITENAME_OUTFIT_15, 
            SUBJECT_SPRITENAME_UNDER_15, 
            SUBJECT_SPRITENAME_ORGANS_15, 
            SUBJECT_SPRITENAME_SKELETON_15, 
            true);
        
        subjectMap.Add(subject04.name, subject04);
        subjectMap.Add(subject14.name, subject14);
        subjectMap.Add(subject15.name, subject15);
        return subjectMap;
    }
}
