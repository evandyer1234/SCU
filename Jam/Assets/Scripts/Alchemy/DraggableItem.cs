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
        [SerializeField] private SpriteRenderer _itemSprite;
        
        private PauseMenuManager _pauseMenuManager;
        private SubjectManager _subjectManager;
        private MiniGameManager _minigameManager;
        private SCUInputAction _scuInputAction;

        [HideInInspector] public Ingredient starterIngredient;
        [HideInInspector] public List<Ingredient> ingredients = new();
        
        private Vector3 offsetItem;
        public bool followMouse;
        private bool isInteractable = false;

        void Awake()
        {
            _scuInputAction = new SCUInputAction();
            _scuInputAction.UI.Enable();
            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                .GetComponent<PauseMenuManager>();

            _itemName.text = "";
        }

        void Start()
        {
            _subjectManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MAIN_EVENT_SYSTEM)
                .GetComponent<SubjectManager>();
        }
        
        void Update()
        {
            if (_pauseMenuManager.isGamePaused()) return;
            if (!isInteractable) return;
            
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
            if (!isInteractable) return;
            
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
                        foreach (var potionIngredient in ingredients)
                        {
                            if (neededIngredients.Find(ing => ing.GetName() == potionIngredient.GetName()) != null)
                            {
                                var ingredientNeeded = neededIngredients.Find(ing =>
                                    ing.GetName() == potionIngredient.GetName());

                                if (ingredientNeeded == null) continue;
                                var ingredientOperationsCovered = ingredientNeeded.GetOperations().All(op =>
                                        potionIngredient.GetOperations().Contains(op));
                                var matchingOperationCount = ingredientNeeded.GetOperations().Count() ==
                                                             potionIngredient.GetOperations().Count();
                                if (ingredientOperationsCovered && matchingOperationCount)
                                {
                                    matchedIngredientCounter++;
                                }
                            }
                        }

                        if (matchedIngredientCounter == 3)
                        {
                            // SUCCESS
                            _minigameManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_MINIGAME_MANAGER)
                                .GetComponent<MiniGameManager>();
                            _minigameManager.MakeJournalSceneButtonAppear();
                            Destroy(gameObject);
                        }
                        else
                        {
                            // FAILURE
                            // need to reload the current pause menu manager, because of previous scene change
                            _pauseMenuManager = GameObject.FindGameObjectWithTag(NamingConstants.TAG_PAUSE_MENU_MANAGER)
                                .GetComponent<PauseMenuManager>();
                            _pauseMenuManager.SetGameOver();
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
                        ingredients.Add(draggableItemIngredient);
                    }
                       
                    Destroy(draggableItem.gameObject);
                    RenderItem();
                }
            }
        }

        public void SetIngredients(List<Ingredient> ingredients)
        {
            this.ingredients = ingredients;
        }

        public List<Ingredient> GetIngredients()
        {
            return ingredients;
        }
        
        public void MakeUndefinedLiquid()
        {
            _itemSprite.sprite = FileLoader.GetSpriteByName(FileConstants.SPR_UNDEFINED_LIQUID);
            isInteractable = true;
            RenderItem();
        }

        public void MakeUndefinedPowder()
        {
            _itemSprite.sprite = FileLoader.GetSpriteByName(FileConstants.SPR_UNDEFINED_POWDER);
            isInteractable = true;
            RenderItem();
        }
        
        public void SetDraggableIngredient(Ingredient selectedIngredient)
        {
            starterIngredient = selectedIngredient;
            ingredients.Add(starterIngredient);
            _itemName.text = starterIngredient.GetName();
            _itemSprite.sprite = starterIngredient.ResolveSpriteIconByIngredientName();
            isInteractable = true;
        }

        private void RenderItem()
        {
            if (ingredients.Count >= 3 && ingredients.Any(ingr => ingr.GetOperations().Contains(IngredientConstants.OPERATION_BOIL)))
            {
                _itemSprite.sprite = FileLoader.GetSpriteByName(FileConstants.SPR_POTION);
            } else if (ingredients.Count >= 3)
            {
                _itemSprite.sprite = FileLoader.GetSpriteByName(FileConstants.SPR_UNDEFINED_POWDER);
            }
            
            string itemConsistenceText = "";
            foreach (var ingredient in ingredients)
            {
                itemConsistenceText += ingredient.GetName() +  " (";
                foreach (var op in ingredient.GetOperations())
                {
                    itemConsistenceText += op + " ";
                }
                itemConsistenceText += ") \n";
            }
            SetItemText(itemConsistenceText);
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