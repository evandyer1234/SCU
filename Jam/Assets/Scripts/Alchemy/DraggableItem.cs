using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TMPro;
using UnityEngine;

namespace Minigames.Alchemy
{
    public class DraggableItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemName;
        
        private PauseMenuManager _pauseMenuManager;
        private SubjectManager _subjectManager;
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
            _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();
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

                CheckCollisionWithOtherDraggableItem(allColliders);
                CheckCollisionWithPatient(allColliders);
            }
        }

        private void CheckCollisionWithPatient(List<Collider2D> colliders)
        {
            foreach (var currColl in colliders)
            {
                if (currColl.gameObject.name == "ClothLayer")
                {
                    var currentSubject = _subjectManager.currentSubject;
                    if (currentSubject != null)
                    {
                        var matchedIngredientCounter = 0;
                        var neededIngredients = currentSubject.neededIngredientsFromPotion;
                        foreach (var potionIngredient in _ingredients)
                        {
                            if (neededIngredients.Find(ing => ing.GetIngredientName() == potionIngredient.GetIngredientName()) != null)
                            {
                                var ingredientNeeded = neededIngredients.Find(ing =>
                                    ing.GetIngredientName() == potionIngredient.GetIngredientName());

                                var ingredientOperationsCovered = ingredientNeeded.GetIngredientOperations().All(op =>
                                        potionIngredient.GetIngredientOperations().Contains(op));
                                if (ingredientOperationsCovered)
                                {
                                    matchedIngredientCounter++;
                                }
                            }
                        }

                        if (matchedIngredientCounter == 3)
                        {
                            // SUCCESS
                            Debug.Log("SUCCESS");
                            Destroy(gameObject);
                        }
                        else
                        {
                            // FAILURE
                            Debug.Log("FAILURE");
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }

        private void CheckCollisionWithOtherDraggableItem(List<Collider2D> colliders)
        {
            foreach (var currColl in colliders)
            {
                if (currColl.GetComponent<DraggableItem>() != null)
                {
                    var draggableItem = currColl.GetComponent<DraggableItem>();
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