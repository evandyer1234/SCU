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

        private SubjectManager _subjectManager;
        
        private bool alchemyShelvesFilled = false;
        
        void Awake()
        {
            _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();
            alchemyShelvesFilled = false;
        }

        void Update()
        {
            LoadAlchemyAndSceneStateMachine();
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