using System.Collections.Generic;
using Helpers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Alchemy
{
    public class MortarPestle : MonoBehaviour
    {
        [SerializeField] private GameObject _addedIngredientContainer;
        [SerializeField] private GameObject _addedIngredientPrefab;
        [SerializeField] private GameObject _producedUndefinedItemPrefab;
        
        private List<Ingredient> _inputIngredients = new();
        private bool _active = true;
        
        private void FixedUpdate()
        {
            if (!_active) return;
            if (_inputIngredients.Count >= 3) return;
            if (RecipeBook.Instance.RecipeBookOpen) return;
            
            var coll = gameObject.GetComponent<Collider2D>();
            var allColliders = new List<Collider2D>();
            Physics2D.OverlapCollider(coll, new ContactFilter2D(), allColliders);

            foreach (var currColl in allColliders)
            {
                if (currColl.GetComponent<DraggableItem>() != null)
                {
                    var draggableItem = currColl.GetComponent<DraggableItem>();
                    if (draggableItem != null && !draggableItem.followMouse)
                    {
                        foreach (var ing in draggableItem.ingredients)
                        {
                            _inputIngredients.Add(ing);
                        }
                        Destroy(draggableItem.gameObject);
                        RenderIngredientPrefabs();
                    }
                }
            }
        }
        
        public void GrindIngredients()
        {
            if (_inputIngredients.Count == 0) return;
            if (RecipeBook.Instance.RecipeBookOpen) return;
            
            foreach (var inputIngredient in _inputIngredients)
            {
                inputIngredient.AddIngredientOperation(IngredientConstants.OPERATION_GRIND);
            }
            
            var undefinedPowder = Instantiate(_producedUndefinedItemPrefab);
            var orgPos = transform.position;
            undefinedPowder.transform.position = new Vector3(orgPos.x, orgPos.y - 5, 10);
            undefinedPowder.GetComponent<DraggableItem>().SetIngredients(_inputIngredients);
            undefinedPowder.GetComponent<DraggableItem>().MakeUndefinedPowder();
            
            _inputIngredients = new();
            RenderIngredientPrefabs();
        }
        
        public void SetInactive()
        {
            this._active = false;
        }
        
        private void RenderIngredientPrefabs()
        {
            foreach(Transform child in _addedIngredientContainer.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var inputIngredient in _inputIngredients)
            {
                var addedIngrGO = Instantiate(_addedIngredientPrefab, _addedIngredientContainer.transform);
                addedIngrGO.GetComponent<AddedIngredient>().itemSprite.sprite = inputIngredient.ResolveSpriteIconByIngredientName();
            }
        }
    }
}
