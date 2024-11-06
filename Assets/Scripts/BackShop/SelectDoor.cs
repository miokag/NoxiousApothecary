using System.Collections.Generic; // Ensure this is included
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDoor : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    private Color hoverColor = Color.yellow;
    public bool canInteract = true; // Make sure canInteract is set to true initially
    public SceneChanger changer;

    public OrderStackHandler orderStackHandler; // Reference to the OrderStackHandler

    private void Start()
    {
        rend = GetComponent<Renderer>();

        // Check if renderer is successfully found
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
        else
        {
            Debug.LogError("Renderer component not found on " + gameObject.name);
        }
    }

    void OnMouseEnter()
    {
        if (canInteract && rend != null)
        {
            Debug.Log("Mouse entered " + gameObject.name);
            rend.material.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        if (canInteract && rend != null)
        {
            Debug.Log("Mouse exited " + gameObject.name);
            rend.material.color = originalColor;
        }
    }

    private void OnMouseDown()
    {
        if (canInteract && rend != null)
        {
            Debug.Log("Mouse clicked on " + gameObject.name);
            canInteract = false; // Disable further interactions
            rend.material.color = originalColor; // Reset color

            if (OrderManager.Instance.CurrentOrder == null) // Access currentOrder through the getter
            {
                // Call the method to initialize order display with the available orders
                List<OrderManager.Order> orders = OrderManager.Instance.GetOrders();
                if (orders.Count > 0)
                {
                    Debug.Log("Orders available: " + orders.Count);
                    orderStackHandler.InitializeOrderDisplay(orders); // Initialize order display with orders
                }
                else
                {
                    Debug.Log("No orders available to display.");
                }
            }
            else
            {
                SceneManager.LoadScene("Exploration");
            }
        }
    }

}
