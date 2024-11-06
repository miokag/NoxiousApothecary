using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    public List<GameObject> customerPrefabs;
    public Transform doorSpawnPoint;

    void Start()
    {
        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        if (customerPrefabs != null && customerPrefabs.Count > 0 && doorSpawnPoint != null)
        {
            // Randomly select a customer prefab from the list
            int randomIndex = Random.Range(0, customerPrefabs.Count);
            GameObject selectedCustomer = customerPrefabs[randomIndex];

            // Instantiate the selected customer at the spawn point
            Instantiate(selectedCustomer, doorSpawnPoint.position, doorSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Customer prefabs list is empty or spawn point is not assigned.");
        }
    }
}
