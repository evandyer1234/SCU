using System.Collections.Generic;
using Helpers;
using TMPro;
using UnityEngine;

namespace Minigames.Alchemy
{
    public class DraggableUndefinedItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemName;
        
        private PauseMenuManager _pauseMenuManager;
        private SCUInputAction _scuInputAction;
        
        private List<Ingredient> _ingredients;
        
        private Vector3 offsetItem;
        private bool followMouse;

        void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();
            _itemName.text = "";
        }

        void Update()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            
            if (followMouse)
            {
                Vector3 mouseWorldPos =  MouseInput.WorldPosition(_scuInputAction);
                KeepSpriteRelativeToMouse(gameObject, mouseWorldPos, offsetItem);
            }

            if (MouseInput.LeftReleased(_scuInputAction))
            {
                followMouse = false;
            }
        }
        
        private void OnMouseOver()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            
            if (MouseInput.LeftClicked(_scuInputAction))
            {
                MouseLeftClick();
            }
            
            if (MouseInput.LeftReleased(_scuInputAction))
            {
                var recentlyInteractedItem = followMouse;
                followMouse = false;
                if (!recentlyInteractedItem) return;
                
                var coll = gameObject.GetComponent<Collider2D>();
                var allColliders = new List<Collider2D>();
                Physics2D.OverlapCollider(coll, new ContactFilter2D(), allColliders);

                foreach (var currColl in allColliders)
                {
                    if (currColl.GetComponent<DraggableUndefinedItem>() != null)
                    {
                        var draggableItem = currColl.GetComponent<DraggableUndefinedItem>();
                        foreach (var draggableItemIngredient in draggableItem.GetIngredients())
                        {
                            _ingredients.Add(draggableItemIngredient);
                        }
                        
                        Destroy(draggableItem.gameObject);
                        string itemConsistenceText = "";
                        foreach (var ingredient in _ingredients)
                        {
                            itemConsistenceText += ingredient.GetIngredientName() +  " (";
                            foreach (var op in ingredient.GetIngredientOperations())
                            {
                                itemConsistenceText += op + " ";
                            }
                            itemConsistenceText += ") \n";
                        }

                        SetItemText(itemConsistenceText);
                    }
                }
            }
        }

        public void SetIngredients(List<Ingredient> ingredients)
        {
            _ingredients = ingredients;
        }

        public List<Ingredient> GetIngredients()
        {
            return _ingredients;
        }
        
        public void MakeUndefinedLiquid()
        {
            _itemName.text = "Undefined Liquid";
            GetComponent<SpriteRenderer>().sprite = FileLoader.GetSpriteByName(FileConstants.SPR_PROTO_LIQUID);
        }

        public void MakeUndefinedPowder()
        {
            _itemName.text = "Undefined Powder";
            GetComponent<SpriteRenderer>().sprite = FileLoader.GetSpriteByName(FileConstants.SPR_PROTO_MINERAL);
        }

        private void SetItemText(string itemText)
        {
            _itemName.text = itemText;
        }
        
        private void MouseLeftClick()
        {
            Vector3 mousepos =  MouseInput.WorldPosition(_scuInputAction);
            offsetItem = (mousepos - gameObject.transform.position);
            followMouse = true;
        }
        
        private void KeepSpriteRelativeToMouse(GameObject spriteRefGo, Vector3 mousePos, Vector3 offset)
        {
            spriteRefGo.transform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, spriteRefGo.transform.position.z);
        }
    }
}