using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Potion/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName; // Name of the ingredient
    public string condition;      // Condition of the ingredient (optional)
    public string description;    // Description of the ingredient
    public Sprite icon;           // Icon to represent the ingredient visually
    public GameObject prefab;     // Prefab file for the 3D game object
}
