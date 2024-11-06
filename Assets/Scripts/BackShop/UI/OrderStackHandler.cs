using UnityEngine;
using TMPro;
using System.Collections.Generic; // Ensure this is included

public class OrderStackHandler : MonoBehaviour
{
    public GameObject orderUIPrefab; // Reference to the UI prefab containing the TMP_Text component
    private TMP_Text orderText; // Reference to the TMP_Text component that displays the order description
    private int currentOrderIndex = 0; // Track the current order index
    private GameObject orderUIInstance; // Reference to the instantiated UI prefab
    private List<OrderManager.Order> currentOrders; // Store the current orders
    public Canvas canvas; // Reference to the Canvas where the UI should be instantiated

    private void Update()
    {
        // Check for left and right arrow key inputs
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CycleToPreviousOrder();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CycleToNextOrder();
        }
    }

    public void InitializeOrderDisplay(List<OrderManager.Order> orders)
    {
        Debug.Log("Initializing order display with " + orders.Count + " orders.");
        currentOrders = orders; // Store the incoming orders
        currentOrderIndex = 0; // Reset to the first order
        InstantiateOrderUI(); // Instantiate the order UI prefab
    }

    private void InstantiateOrderUI()
    {
        if (orderUIPrefab != null && canvas != null)
        {
            orderUIInstance = Instantiate(orderUIPrefab, canvas.transform); // Instantiate under the Canvas
            orderText = orderUIInstance.GetComponentInChildren<TMP_Text>(); // Find TMP_Text in the prefab
            UpdateOrderText(); // Update the display with the first order
        }
        else
        {
            if (orderUIPrefab == null)
            {
                Debug.LogError("Order UI prefab is not assigned in the inspector.");
            }
            if (canvas == null)
            {
                Debug.LogError("Canvas is not assigned in the inspector.");
            }
        }
    }

    private void CycleToPreviousOrder()
    {
        // Decrement the index and wrap around if necessary
        currentOrderIndex--;
        if (currentOrderIndex < 0)
        {
            currentOrderIndex = currentOrders.Count - 1; // Wrap to last order
        }
        UpdateOrderText(); // Update the display
    }

    private void CycleToNextOrder()
    {
        // Increment the index and wrap around if necessary
        currentOrderIndex++;
        if (currentOrderIndex >= currentOrders.Count)
        {
            currentOrderIndex = 0; // Wrap to first order
        }
        UpdateOrderText(); // Update the display
    }

    public void UpdateOrderText()
    {
        // Get the current order and update the text
        if (currentOrders != null && currentOrders.Count > 0)
        {
            orderText.text = $"{currentOrders[currentOrderIndex].orderDescription}";
            OrderManager.Instance.SetCurrentOrder(currentOrders[currentOrderIndex].orderDescription); // Set the current order in OrderManager
        }
        else
        {
            orderText.text = "No orders available"; // Handle empty orders case
        }
    }

    public string GetCurrentOrderDescription()
    {
        if (currentOrders != null && currentOrders.Count > 0)
        {
            return currentOrders[currentOrderIndex].orderDescription;
        }
        return null;
    }



}
