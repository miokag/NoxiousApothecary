using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomerInfo
{
    public string name;
    public PotionInfo potionOrder;
    public int rating;

    public CustomerInfo(string name, PotionInfo potionOrder, int rating)
    {
        this.name = name;
        this.potionOrder = potionOrder;
        this.rating = rating;
    }
}


