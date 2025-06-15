using Helpers;
using Minigames.Alchemy;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Managers
{
    public class AlchemyManager : MonoBehaviour
    {
        [SerializeField] private DraggableItem _draggableFirstIngredient;
        [SerializeField] private DraggableItem _draggableSecondIngredient;
        [SerializeField] private DraggableItem _draggableThirdIngredient;
        [SerializeField] private GameObject _treatPatientButton;
        [SerializeField] private GameObject _resetPreparationButton;

        private SubjectManager _subjectManager;
        
        private bool finalPotionBrewed = false;
        private int rotatingSelectedIngredientSlot = 0;
        
        void Awake()
        {
            _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();
            _treatPatientButton.SetActive(false);
            _resetPreparationButton.SetActive(true);
            finalPotionBrewed = false;
        }

        private void FixedUpdate()
        {
            CheckBrewedPotionCondition();
        }

        private void CheckBrewedPotionCondition()
        {
            if (finalPotionBrewed) return;
            
            var items = FindObjectsOfType<DraggableItem>();
            foreach (var item in items)
            {
                if (item.GetIngredients() == null) continue;
                if (item.GetIngredients().Count >= 3)
                {
                    DontDestroyOnLoad(item.gameObject);
                    _treatPatientButton.SetActive(true);
                    _resetPreparationButton.SetActive(false);
                    finalPotionBrewed = true;
                    break;
                }
            }
        }

        public void MoveToPatientPageScene()
        {
            _subjectManager.GetSCUSceneManager().TransitionToScene(NamingConstants.SCENE_MAIN_MINIGAME);
            if (_subjectManager.currentSubject != null)
            {
                _subjectManager.LaunchPatientPageInPotionMode(_subjectManager.currentSubject.name);
            }
        }

        public void ResetPotionScene()
        {
            _subjectManager.GetSCUSceneManager().TransitionToScene(NamingConstants.SCENE_MAIN_ALCHEMY, true);
        }

        public void SetIngredientSelected(Ingredient ingredient)
        {
            switch (rotatingSelectedIngredientSlot)
            {
                case 0:
                    if (_draggableFirstIngredient == null) return;
                    _draggableFirstIngredient.SetDraggableIngredient(ingredient);
                    break;
                case 1:
                    if (_draggableSecondIngredient == null) return;
                    _draggableSecondIngredient.SetDraggableIngredient(ingredient);
                    break;
                case 2:
                    if (_draggableThirdIngredient == null) return;
                    _draggableThirdIngredient.SetDraggableIngredient(ingredient);
                    break;
                default:
                    Debug.LogWarning("FAILED TO MATCH SELECTED INGREDIENT TO NEXT SLOT");
                    break;
            }

            rotatingSelectedIngredientSlot++;
            if (rotatingSelectedIngredientSlot >= 3)
            {
                rotatingSelectedIngredientSlot = 0;
            }
        }
    }
}