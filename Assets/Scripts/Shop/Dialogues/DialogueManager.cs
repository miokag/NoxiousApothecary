using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private List<DialogueInfo> potionDialogues;

    private void Start()
    {
        InitializeDialogues();
    }

    private void InitializeDialogues()
    {
        potionDialogues = new List<DialogueInfo>();

        PotionList potionList = FindObjectOfType<PotionList>();
        if (potionList == null)
        {
            Debug.LogWarning("PotionList not found in the scene!");
            return;
        }

        foreach (var potion in potionList.potions)
        {
            DialogueInfo dialogueInfo = new DialogueInfo(potion);

            switch (potion.name)
            {
                case "Damage Resistant Potion":
                    dialogueInfo.AddCustomerDialogue("Talia", new string[]
                    {
                        "Talia: I’m heading into a dangerous cave tonight. I need a potion that’ll lessen the damage I take from monsters. Give me something strong!"
                    });
                    dialogueInfo.AddCustomerDialogue("Gorak", new string[]
                    {
                        "Gorak: I’m heading into a dangerous cave tonight. I need a potion that’ll lessen the damage I take from monsters. Give me something strong!"
                    });
                    dialogueInfo.AddOrderDescription("Talia", "Talia ordered a Damage Resistant Potion for protection against monsters.");
                    dialogueInfo.AddOrderDescription("Gorak", "Gorak ordered a Damage Resistant Potion to survive the cave.");
                    break;

                case "Health Potion":
                    dialogueInfo.AddCustomerDialogue("Talia", new string[]
                    {
                        "Talia: I’ve been working my back off at the forge! I need something to ease this pain so I can swing my hammer without feeling like an old woman!"
                    });
                    dialogueInfo.AddCustomerDialogue("Gorak", new string[]
                    {
                        "Gorak: I’ve been working my back off at the forge! I need something to ease this pain so I can swing my hammer without feeling like an old woman!"
                    });
                    dialogueInfo.AddOrderDescription("Talia", "Talia ordered a Health Potion to relieve her back pain.");
                    dialogueInfo.AddOrderDescription("Gorak", "Gorak ordered a Health Potion for his aching back after long hours of work.");
                    break;
            }

            potionDialogues.Add(dialogueInfo);
        }
    }

    public string GetDialogue(string customerName, PotionInfo potion)
    {
        foreach (var dialogueInfo in potionDialogues)
        {
            if (dialogueInfo.potion.name == potion.name && dialogueInfo.customerDialogues.TryGetValue(customerName, out var dialogues))
            {
                return dialogues[Random.Range(0, dialogues.Length)];
            }
        }

        return "I have nothing to say...";
    }

    public string GetOrderDescription(string customerName, PotionInfo potion)
    {
        foreach (var dialogueInfo in potionDialogues)
        {
            if (dialogueInfo.potion.name == potion.name)
            {
                return dialogueInfo.GetOrderDescription(customerName);
            }
        }

        return "No order description available.";
    }
}
