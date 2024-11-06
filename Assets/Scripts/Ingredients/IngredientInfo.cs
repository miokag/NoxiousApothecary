using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class IngredientInfo
{
    public string name;
    public string type;
    public string description;

    public IngredientInfo(string name, string type, string description)
    {
        this.name = name;
        this.type = type;
        this.description = description;
    }
}
