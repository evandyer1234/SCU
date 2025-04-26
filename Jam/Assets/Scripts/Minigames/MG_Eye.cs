using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class MG_Eye : MiniGameBase
{
    [SerializeField] private List<GameObject> selectableEyes;
    [SerializeField] private GameObject lEye, rEye;

    [SerializeField] internal Iris intactEye;

    [HideInInspector] public Sprite correctsprite;
     public SphereCollider sc;

    [SerializeField] private List<Sprite> irisColors;
    
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pickupEyeSound;
    [SerializeField] private AudioClip _releaseEyeSound;

    private void OnEnable()
    {
        if (lEye.GetComponentInChildren<Iris>().isIntactEye)
        {
            intactEye = lEye.GetComponentInChildren<Iris>();
        }
        else
        {
            intactEye = rEye.GetComponentInChildren<Iris>();
        }
        
        RandomizeIrisColors();

        correctsprite = lEye.GetComponentInChildren<Iris>().gameObject.GetComponent<SpriteRenderer>().sprite;
        
        _audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (miniGameStage == Stage.Stage01)
        {
            // DO STAGE 1 STUFF HERE
            // 1. REMOVE INFECTED EYE FROM EYE SOCKET
        } else if (miniGameStage == Stage.Stage02)
        {
            // DO STAGE 2 STUFF HERE
            // 1. MAKE UNINFECTED EYE MOVE AT RANDOM, FAST INTERVALS, TO RANDOM LOCATIONS
            // 2. CHECK IF SELECTED GLASS EYE IS CORRECT COLOR
            // 3. IF NOT, KNOCK DOWN TIME
            // 4. IF SO , COMPLETE MINIGAME
        }
    }

    public void RemoveEye()
    {
        SwapStage(2);
    }


    void RandomizeIrisColors()
    {
        for (int i = 0; i < selectableEyes.Count; i++)
        {
            selectableEyes[i].GetComponentInChildren<Iris>().gameObject.GetComponent<SpriteRenderer>().sprite = irisColors[Random.Range(0, irisColors.Count)];
            Debug.Log("" + selectableEyes[i].GetComponentInChildren<Iris>().gameObject.GetComponent<SpriteRenderer>().sprite);
        }
    }

    public void PickupEyeSound()
    {
        _audioSource.loop = true;
        _audioSource.clip = _pickupEyeSound;
        _audioSource.Play();
    }

    public void ReleaseEyeSound()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_releaseEyeSound);
    }
}
