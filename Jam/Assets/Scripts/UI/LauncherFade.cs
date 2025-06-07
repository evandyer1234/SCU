using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LauncherFade : MonoBehaviour
    {
        [SerializeField] private int playDuration;
        [SerializeField] private GameObject cursorToHide;
        [SerializeField] private GameObject soundTrackToHide;
        [SerializeField] private GameObject playerToHide;

        private SubjectManager _subjectManager;        
        private SpriteRenderer logoRenderer;
        private int frame;
        private float fadeDivider = 500f;
        
        void Awake()
        {
            logoRenderer = GetComponent<SpriteRenderer>();
            var c = logoRenderer.color;
            logoRenderer.color = new Color(c.r, c.g, c.b, 0);
            cursorToHide.SetActive(false);
            soundTrackToHide.SetActive(false);
            playerToHide.SetActive(false);
            _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();
        }

        void Start()
        {
            Invoke(nameof(InitiateGame), (playDuration / 60f));
        }
        
        void Update()
        {
            frame++;

            if (frame <= (playDuration / 4))
            {
                return;
            }
            
            var c = logoRenderer.color;
            var alpha = c.a;
            if (frame <= ((playDuration * 8) / 4) * 3)
            {
                if (c.a <= 1f)
                {
                    alpha += (Time.deltaTime * 0.7f) / (playDuration / fadeDivider);
                }
                else
                {
                    alpha = 1;
                }
            }
            else
            {
                if (c.a >= 0f)
                {
                    alpha -= Time.deltaTime / (playDuration / fadeDivider);
                }
                else
                {
                    alpha = 0;
                }
            }
            logoRenderer.color = new Color(c.r, c.g, c.b, alpha);
        }
        
        private void InitiateGame()
        {
            cursorToHide.SetActive(true);
            soundTrackToHide.SetActive(true);
            playerToHide.SetActive(true);
            _subjectManager.GetSCUSceneManager().TransitionToScene(NamingConstants.SCENE_ID_MAIN_MENU);
        }
    }
}