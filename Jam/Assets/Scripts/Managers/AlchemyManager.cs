using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Minigames;
using Minigames.Alchemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class AlchemyManager : MonoBehaviour
    {
        [SerializeField] private AlchemyShelf upperShelf;
        [SerializeField] private AlchemyShelf middleShelf;
        [SerializeField] private AlchemyShelf lowerShelf;
        [SerializeField] private DraggableIngredient _draggablePlantIngredient;
        [SerializeField] private DraggableIngredient _draggableLiquidIngredient;
        [SerializeField] private DraggableIngredient _draggableMineralIngredient;
        [SerializeField] private GameObject _treatPatientButton;
        [SerializeField] private GameObject _resetPreparationButton;

        private SubjectManager _subjectManager;
        
        private bool alchemyShelvesFilled = false;
        private bool finalPotionBrewed = false;
        
        void Awake()
        {
            _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();
            alchemyShelvesFilled = false;
            _treatPatientButton.SetActive(false);
            _resetPreparationButton.SetActive(true);
        }

        void Update()
        {
            LoadAlchemyAndSceneStateMachine();
        }

        private void FixedUpdate()
        {
            CheckBrewedPotionCondition();
        }

        private void CheckBrewedPotionCondition()
        {
            var items = FindObjectsOfType<DraggableItem>();
            foreach (var item in items)
            {
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

        private void LoadAlchemyAndSceneStateMachine()
        {
            if (alchemyShelvesFilled) return;
        
            FillShelvesWithRespectiveIngredients(_subjectManager.GetAllIngredients().Values.ToList());
            alchemyShelvesFilled = true;
        }
        
        public void FillShelvesWithRespectiveIngredients(List<Ingredient> ingredients)
        {
            upperShelf.FillShelfByType(ingredients);
            middleShelf.FillShelfByType(ingredients);
            lowerShelf.FillShelfByType(ingredients);
        }

        public void ResetPotionScene()
        {
            _subjectManager.GetSCUSceneManager().TransitionToScene(SceneManager.GetActiveScene().name, true);
        }

        public void SetIngredientSelected(Ingredient ingredient)
        {
            switch (ingredient.GetIngredientType())
            {
                case IngredientType.PLANT:
                    if (_draggablePlantIngredient == null) return;
                    _draggablePlantIngredient.SetDraggableIngredient(ingredient);
                    break;
                case IngredientType.LIQUID:
                    if (_draggableLiquidIngredient == null) return;
                    _draggableLiquidIngredient.SetDraggableIngredient(ingredient);
                    break;
                case IngredientType.MINERAL:
                    if (_draggableMineralIngredient == null) return;
                    _draggableMineralIngredient.SetDraggableIngredient(ingredient);
                    break;
                default:
                    Debug.LogWarning("FAILED TO MATCH SELECTED INGREDIENT TYPE");
                    break;
            }
        }
    }
}