using System.Collections.Generic;

[System.Serializable]
public class DialogueInfo
{
    public PotionInfo potion;  // Reference to the potion
    public Dictionary<string, string[]> customerDialogues; // Key: customer name, Value: array of dialogues
    public Dictionary<string, string> orderDescriptions; // Key: customer name, Value: order description

    public DialogueInfo(PotionInfo potion)
    {
        this.potion = potion;
        customerDialogues = new Dictionary<string, string[]>();
        orderDescriptions = new Dictionary<string, string>();
    }

    // Add a customer dialogue
    public void AddCustomerDialogue(string customerName, string[] dialogues)
    {
        customerDialogues[customerName] = dialogues;
    }

    // Add an order description
    public void AddOrderDescription(string customerName, string description)
    {
        orderDescriptions[customerName] = description;
    }

    // Get order description for a specific customer
    public string GetOrderDescription(string customerName)
    {
        return orderDescriptions.TryGetValue(customerName, out var description) ? description : "No order description available.";
    }
}
