using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    [System.Serializable]
    public class Order
    {
        public string customerName;
        public string orderDescription;
        public PotionInfo potion; // Assuming PotionInfo is your potion class
    }

    private List<Order> orders = new List<Order>();
    private Order currentOrder; // Store the current order

    public Order CurrentOrder // Public getter for currentOrder
    {
        get { return currentOrder; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start() // Add test orders here
    {
        AddTestOrders();
    }

    public void AddTestOrders()
    {
        AddOrder("Alice", "Healing Potion", null); // Assuming PotionInfo is optional for testing
        AddOrder("Bob", "Mana Potion", null);
        AddOrder("Charlie", "Strength Potion", null);
        AddOrder("Diana", "Invisibility Potion", null);
        Debug.Log("Test orders added.");
    }

    public void AddOrder(string customerName, string orderDescription, PotionInfo potion)
    {
        Order newOrder = new Order()
        {
            customerName = customerName,
            orderDescription = orderDescription,
            potion = potion
        };
        orders.Add(newOrder);
    }

    public void SetCurrentOrder(string orderDescription)
    {
        currentOrder = orders.Find(order => order.orderDescription == orderDescription);
        if (currentOrder != null)
        {
            Debug.Log($"Current order set to: {currentOrder.orderDescription}");
        }
        else
        {
            Debug.LogWarning("Order not found!");
        }
    }

    public List<Order> GetOrders() // New method to get orders
    {
        return orders;
    }
}
