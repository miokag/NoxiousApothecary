using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionInfo
{
    public string name;
    public List<Ingredient> ingredients; // List of Ingredient ScriptableObjects
    public string[] process;               // Process steps for creating the potion

    public PotionInfo(string name, List<Ingredient> ingredients, string[] process)
    {
        this.name = name;
        this.ingredients = ingredients;
        this.process = process;
    }
}
