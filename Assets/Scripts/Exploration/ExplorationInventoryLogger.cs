using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationInventoryLogger : MonoBehaviour
{
    private string currentOrder;
    void Start()
    {
        // Check if InventoryManager exists and log its contents
        if (InventoryManager.Instance != null)
        {
            List<string> inventoryItems = InventoryManager.Instance.GetAllItems();
            Debug.Log("Current Inventory Items:");
            foreach (string item in inventoryItems)
            {
                currentOrder = item;
            }
        }
        else
        {
            Debug.LogWarning("InventoryManager instance not found.");
        }
    }
}
