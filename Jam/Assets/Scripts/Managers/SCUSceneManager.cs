using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SCUSceneManager : MonoBehaviour
    {
        public static bool IsMainMenuScene()
        {
            return SceneManager.GetActiveScene().buildIndex == NamingConstants.SCENE_ID_MAIN_MENU;
        }
        
        public static bool IsMinigameScene()
        {
            return SceneManager.GetActiveScene().name == NamingConstants.SCENE_MAIN_MINIGAME;
        }
        
        public static bool IsAlchemyScene()
        {
            return SceneManager.GetActiveScene().name == NamingConstants.SCENE_MAIN_ALCHEMY;
        }
        
        public void TransitionToScene(int sceneIndex, bool skipFade = false)
        {
            var sceneFade = ResolveSceneFadeSafely();
            if (sceneFade != null && !skipFade)
            {
                sceneFade.FadeOut(() =>
                {
                    SceneManager.LoadScene(sceneIndex);
                });
            }
            else
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }

        public void TransitionToScene(string sceneName, bool skipFade = false)
        {
            var sceneFade = ResolveSceneFadeSafely();
            if (sceneFade != null && !skipFade)
            {
                sceneFade.FadeOut(() =>
                {
                    SceneManager.LoadScene(sceneName);
                });
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        private SceneFade ResolveSceneFadeSafely()
        {
            SceneFade sceneFade = null;
            GameObject sfgo = GameObject.FindGameObjectWithTag(NamingConstants.TAG_SCENE_FADE);
            if (sfgo != null)
            {
                sceneFade = sfgo.GetComponent<SceneFade>();
            }
            return sceneFade;
        }
    }
}