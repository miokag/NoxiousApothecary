using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
public class CustomerInteractions : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    private Color hoverColor = Color.yellow;
    private CustomerActions customerActions;
    public bool takenOrder;
    public bool canInteract; // New flag to track interaction state

    private OrderHandler orderHandler; // Reference to OrderHandler

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        customerActions = GetComponent<CustomerActions>();
        canInteract = true; // Initially, the customer can be interacted with

        // Get the OrderHandler from the scene (make sure it's added to a GameObject)
        orderHandler = FindObjectOfType<OrderHandler>();
    }

    void OnMouseEnter()
    {
        if (canInteract && !takenOrder && !customerActions.isMoving)
        {
            rend.material.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        if (canInteract && !takenOrder)
        {
            rend.material.color = originalColor;
        }
    }

    void OnMouseDown()
    {
        if (!takenOrder && canInteract)
        {
            if (!customerActions.isMoving)
            {
                orderHandler.TakeOrder(customerActions); // Call TakeOrder from OrderHandler
                rend.material.color = originalColor;
                canInteract = false;
                takenOrder = true; // Mark as taken
            }
        }
        else if (takenOrder)
        {
            StartCoroutine(HandleOrderCompletion());
        }
    }

    private IEnumerator HandleOrderCompletion()
    {
        Destroy(orderHandler.dialogueBox); // Destroy the dialogue box when the order is completed
        takenOrder = false;

        // Move the customer to the side
        yield return StartCoroutine(customerActions.MoveToSideCoroutine(2f, 1f)); // Move 2 units over 1 second

        // After the coroutine finishes, instantiate the order note
        orderHandler.InstantiateOrderNote(customerActions.GetOrderDescription());
    }
}
