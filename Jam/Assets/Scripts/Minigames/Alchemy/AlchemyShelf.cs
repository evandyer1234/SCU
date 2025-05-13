using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Minigames.Alchemy
{
    public class AlchemyShelf : MonoBehaviour
    {
        [SerializeField] private GameObject shelfInventory;
        [SerializeField] private IngredientType ingredientType;
        [SerializeField] private GameObject ingredientSlotPrefab;
        [SerializeField] private GameObject emptySlotPrefab;
        [SerializeField] private Canvas parentCanvas;

        private int maxAmountSlots = 14;
        
        private List<Ingredient> ingredientsInShelf = new();

        void Awake()
        {
            ClearInventory();
        }
        
        public void FillShelfByType(List<Ingredient> collectedIngredients)
        {
            foreach (var currIngredient in collectedIngredients)
            {
                if (currIngredient.GetIngredientType() == ingredientType)
                {
                    ingredientsInShelf.Add(currIngredient);
                }
            }

            RenderShelf();
        }

        private void RenderShelf()
        {
            for (int i = 0; i < maxAmountSlots; i++)
            {
                if (ingredientsInShelf.Count > i)
                {
                    var ingrSlot = Instantiate(ingredientSlotPrefab, shelfInventory.transform);
                    ingrSlot.transform.localScale *= (1/parentCanvas.scaleFactor);
                    switch (ingredientsInShelf[i].GetIngredientType())
                    {
                        case IngredientType.PLANT:
                            ingrSlot.GetComponent<SpriteRenderer>().sprite = FileLoader.GetSpriteByName("proto_plant");
                            break;
                        case IngredientType.LIQUID:
                            ingrSlot.GetComponent<SpriteRenderer>().sprite = FileLoader.GetSpriteByName("proto_liquid");
                            break;
                        case IngredientType.MINERAL:
                            ingrSlot.GetComponent<SpriteRenderer>().sprite = FileLoader.GetSpriteByName("proto_mineral");
                            break;
                    }
                }
                else
                {
                    Instantiate(emptySlotPrefab, shelfInventory.transform);
                }
            }
        }
        
        private void ClearInventory()
        {
            foreach (Transform slot in shelfInventory.transform)
            {
                Destroy(slot.gameObject);
            }
        }
    }
}