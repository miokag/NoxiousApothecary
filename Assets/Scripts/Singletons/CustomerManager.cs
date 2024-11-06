using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;

    public List<CustomerInfo> customers = new List<CustomerInfo>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void AddCustomer(CustomerInfo customer)
    {
        customers.Add(customer);
    }

    public void ClearCustomers()
    {
        customers.Clear();
    }
}
