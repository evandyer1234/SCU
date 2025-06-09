using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Alchemy
{
    public class Cauldron : MonoBehaviour
    {
        [SerializeField] private GameObject _addedIngredientContainer;
        [SerializeField] private GameObject _addedIngredientPrefab;
        [SerializeField] private GameObject _producedUndefinedItemPrefab;
        
        private List<Ingredient> _inputIngredients = new();
        
        private void FixedUpdate()
        {
            if (_inputIngredients.Count >= 3) return;
            
            var coll = gameObject.GetComponent<Collider2D>();
            var allColliders = new List<Collider2D>();
            Physics2D.OverlapCollider(coll, new ContactFilter2D(), allColliders);

            foreach (var currColl in allColliders)
            {
                if (currColl.GetComponent<DraggableIngredient>() != null)
                {
                    var draggableIng = currColl.GetComponent<DraggableIngredient>();
                    if (draggableIng.ingredient != null && !draggableIng.followMouse)
                    {
                        _inputIngredients.Add(draggableIng.ingredient);
                        Destroy(draggableIng.gameObject);
                        RenderIngredientPrefabs();
                    }
                }
            }
        }

        public void BoilIngredients()
        {
            if (_inputIngredients.Count == 0) return;

            foreach (var inputIngredient in _inputIngredients)
            {
                inputIngredient.AddIngredientOperation(IngredientConstants.OPERATION_BOIL);
            }
            
            var undefinedLiquid = Instantiate(_producedUndefinedItemPrefab);
            var orgPos = transform.position;
            undefinedLiquid.transform.position = new Vector3(orgPos.x, orgPos.y - 5, 10);
            undefinedLiquid.GetComponent<DraggableUndefinedItem>().MakeUndefinedLiquid();
            undefinedLiquid.GetComponent<DraggableUndefinedItem>().SetIngredients(_inputIngredients);
            
            _inputIngredients = new();
            RenderIngredientPrefabs();
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
                switch (inputIngredient.GetIngredientType())
                {
                    case IngredientType.PLANT:
                        addedIngrGO.GetComponent<Image>().sprite = FileLoader.GetSpriteByName("proto_plant");
                        break;
                    case IngredientType.LIQUID:
                        addedIngrGO.GetComponent<Image>().sprite = FileLoader.GetSpriteByName("proto_liquid");
                        break;
                    case IngredientType.MINERAL:
                        addedIngrGO.GetComponent<Image>().sprite = FileLoader.GetSpriteByName("proto_mineral");
                        break;
                }
            }
        }
    }
}
