using System.Collections.Generic;
using UnityEngine;

public class PotionList : MonoBehaviour
{
    public List<PotionInfo> potions; // List of available potions
    private IngredientList ingredientList;

    private void Start()
    {
        ingredientList = FindObjectOfType<IngredientList>();
        if (ingredientList == null)
        {
            Debug.LogWarning("IngredientList not found in the scene!");
        }
    }

    public PotionInfo GetRandomPotion()
    {
        if (potions.Count > 0)
        {
            int randomIndex = Random.Range(0, potions.Count);
            return potions[randomIndex];
        }
        return null;
    }

    public List<Ingredient> GetPotionIngredients(PotionInfo potion)
    {
        return potion.ingredients; // Directly return the list of ingredients
    }
}
