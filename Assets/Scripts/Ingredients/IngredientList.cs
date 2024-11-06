using System.Collections.Generic;
using UnityEngine;

public class IngredientList : MonoBehaviour
{
    public static IngredientList Instance { get; private set; }

    public List<Ingredient> ingredients; // List of available ingredients

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy any duplicate IngredientLists
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance alive across scenes
        }
    }

    // Method to get an ingredient by name
    public Ingredient GetIngredientByName(string name)
    {
        return ingredients.Find(ingredient => ingredient.ingredientName == name);
    }
}
