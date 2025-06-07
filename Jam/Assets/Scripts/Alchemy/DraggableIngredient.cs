using Helpers;
using UnityEngine;

namespace Minigames.Alchemy
{
    public class DraggableIngredient : MonoBehaviour
    {
        [SerializeField] private IngredientType _ingredientType;
        
        private Ingredient _ingredient;

        private Sprite _plantSprite;
        private Sprite _liquidSprite;
        private Sprite _mineralSprite;
        
        void Awake()
        {
            _plantSprite = FileLoader.GetSpriteByName(FileConstants.SPR_PROTO_PLANT);
            _liquidSprite = FileLoader.GetSpriteByName(FileConstants.SPR_PROTO_LIQUID);
            _mineralSprite = FileLoader.GetSpriteByName(FileConstants.SPR_PROTO_MINERAL);
        }
        
        public void SetDraggableIngredient(Ingredient ingredient)
        {
            if (_ingredientType == ingredient.GetIngredientType())
            {
                this._ingredient = ingredient;
            }
            
            // TODO: rework this with actual ingredient sprites someday
            switch (_ingredientType)
            {
                case IngredientType.PLANT:
                    GetComponent<SpriteRenderer>().sprite = _plantSprite;
                    break;
                case IngredientType.LIQUID:
                    GetComponent<SpriteRenderer>().sprite = _liquidSprite;
                    break;
                case IngredientType.MINERAL:
                    GetComponent<SpriteRenderer>().sprite = _mineralSprite;
                    break;
                default:
                    Debug.LogWarning("FAILED TO ASSIGN SPRITE ON DRAGGABLE INGREDIENT");
                    break;
            }
        }
    }
}