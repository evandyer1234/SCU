using Helpers;
using TMPro;
using UnityEngine;

namespace Minigames.Alchemy
{
    public class DraggableIngredient : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ingredientName;
        
        private PauseMenuManager _pauseMenuManager;
        private SCUInputAction _scuInputAction;
        
        public Ingredient ingredient;
        
        private Vector3 offsetIngredient;
        public bool followMouse;
        
        void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
            
            _ingredientName.text = "";
        }

        void Update()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            if (ingredient == null) return;
            
            if (followMouse)
            {
                Vector3 mouseWorldPos =  MouseInput.WorldPosition(_scuInputAction);
                KeepSpriteRelativeToMouse(gameObject, mouseWorldPos, offsetIngredient);
            }

            if (MouseInput.LeftReleased(_scuInputAction))
            {
                followMouse = false;
            }
        }

        private void OnMouseOver()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            if (ingredient == null) return;
            
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }
        }

        public void SetDraggableIngredient(Ingredient selectedIngredient)
        {
            ingredient = selectedIngredient;
            _ingredientName.text = selectedIngredient.GetIngredientName();
            GetComponent<SpriteRenderer>().sprite = ingredient.ResolveSpriteByIngredientName();
        }
        
        private void MouseLeftClick()
        {
            Vector3 mousepos =  MouseInput.WorldPosition(_scuInputAction);
            offsetIngredient = (mousepos - gameObject.transform.position);
            followMouse = true;
        }
        
        private void KeepSpriteRelativeToMouse(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            spriteRefGo.transform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, spriteRefGo.transform.position.z);
        }


    }
}