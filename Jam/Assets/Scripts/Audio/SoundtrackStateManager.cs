using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundtrackStateManager : MonoBehaviour
{
    private void Awake()
    {
        AkBankManager.LoadBank("MainSoundBank", false, false);
        Debug.Log("MainSoundBank Loaded");
    }
    private void OnEnable()
    {
        AkSoundEngine.PostEvent("StartMusic", gameObject);
        Debug.Log("StartMusic Event Triggered");
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                AkSoundEngine.ResetRTPCValue("IsLevelSelect");
                break;
            case 1:
                Debug.Log("MainMenu Audio State Change");
                AkSoundEngine.SetState("Location", "MainMenu");
                break;
            case 2:
                Debug.Log("Minigame Audio State Change");
                AkSoundEngine.SetState("Location", "MiniGame");
                break;
            case 3:
                Debug.Log("Alchemy Audio State Change");
                AkSoundEngine.SetState("Location", "Alchemy");
                break;
            case 4:
                Debug.Log("Journals Audio State Change");
                AkSoundEngine.SetState("Location", "Journals");
                break;
            default:
                break;
        }
    }

    private void OnSceneUnloaded(Scene scene)
    { 
        switch (scene.buildIndex)
        {
            case 0:
                break;
            case 1:
                StartCoroutine(DelayResetMainMenu());
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        AkBankManager.UnloadBank("MainSoundBank");
    }

    private IEnumerator DelayResetMainMenu()
    {
        yield return new WaitForSeconds(3);
        AkSoundEngine.ResetRTPCValue("IsLevelSelect");
    }
}
