using UnityEngine;
using System.Collections; // Add this line

public class CustomerActions : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool isMoving = true;

    public string customerName;
    public int customerRating;

    private PotionInfo potionOrder;
    private PotionList potionList;
    private DialogueManager dialogueManager;
    private CustomerInteractions customerInteractions;

    void Start()
    {
        potionList = FindObjectOfType<PotionList>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        customerInteractions = GetComponent<CustomerInteractions>(); // Get the CustomerInteractions component

        if (potionList != null)
        {
            potionOrder = potionList.GetRandomPotion();

            if (potionOrder != null)
            {
                CustomerInfo newCustomer = new CustomerInfo(customerName, potionOrder, customerRating);
                CustomerManager.Instance.AddCustomer(newCustomer);

                // Get the dialogue based on customer name and potion
                string customerDialogue = dialogueManager.GetDialogue(customerName, potionOrder);
                string orderDescription = dialogueManager.GetOrderDescription(customerName, potionOrder); // Get the order description

                Debug.Log("Potion Order for " + customerName + ": " + potionOrder.name);
                Debug.Log("Ingredients: " + string.Join(", ", potionOrder.ingredients));
                Debug.Log("Process steps: " + string.Join(", ", potionOrder.process));
                Debug.Log("Customer Dialogue: " + customerDialogue);
                Debug.Log("Order Description: " + orderDescription); // Log the order description
            }
        }
        else
        {
            Debug.LogWarning("PotionList not found in the scene!");
        }
    }

    void Update()
    {
        if (isMoving)
        {
            MoveForward();
        }
    }

    void MoveForward()
    {
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public IEnumerator MoveToSideCoroutine(float distance, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;
        Vector3 targetPosition = startingPosition + (Vector3.left * distance);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.position = targetPosition; // Ensure final position is set
    }

    void OnTriggerEnter(Collider other)
    {
        isMoving = false;
    }

    public string GetCustomerDialogue()
    {
        // Assuming potionOrder is set
        if (dialogueManager != null && potionOrder != null)
        {
            return dialogueManager.GetDialogue(customerName, potionOrder);
        }
        return "I have nothing to say..."; // Default message if dialogue is not found
    }

    public string GetOrderDescription()
    {
        if (dialogueManager != null && potionOrder != null)
        {
            return dialogueManager.GetOrderDescription(customerName, potionOrder); // Ensure this fetches the correct description
        }
        return "No order description available."; // Fallback message if no order is found
    }

    public PotionInfo GetPotionOrder()
    {
        return potionOrder; // Return the potion order
    }


}
