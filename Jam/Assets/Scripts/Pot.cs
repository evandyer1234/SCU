using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public List<Ingredient.ItemVal> drops = new List<Ingredient.ItemVal>();
    
   public void Drain()
   {
        drops.Clear();
   }

   public void Mix()
   {
        GameModeManager gmm = FindObjectOfType<GameModeManager>();
        if (gmm != null ) 
        { 
            foreach (Ingredient.ItemVal i in drops) 
            {
                if (gmm.neededItems.Contains(i))
                {
                    gmm.neededItems.Remove(i);
                }
                else
                {
                    gmm.neededItems.Clear();
                }
            }
            if (gmm.neededItems.Count == 0)
            {
                gmm.Win();
                //win code
            }
        }

   }

}
