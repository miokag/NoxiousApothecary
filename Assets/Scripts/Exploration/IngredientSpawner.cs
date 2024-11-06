using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [System.Serializable]
    public class IngredientArea
    {
        public Ingredient ingredientData; // Reference to the ScriptableObject containing ingredient data
        public int ingredientCount = 10; // Number of ingredients to spawn
        public Collider areaCollider; // Reference to the area collider
    }

    public List<IngredientArea> ingredientAreas; // List of ingredient areas
    public float spawnRadius = 0.5f; // Radius to check for overlaps (adjust based on prefab size)
    public LayerMask ingredientLayerMask; // Layer to detect ingredients to prevent overlaps

    void Start()
    {
        SpawnIngredientsInAllAreas();
    }

    void SpawnIngredientsInAllAreas()
    {
        foreach (IngredientArea area in ingredientAreas)
        {
            for (int i = 0; i < area.ingredientCount; i++)
            {
                Vector3 randomPosition = GetValidSpawnPoint(area.areaCollider);

                // Instantiate the prefab from the ScriptableObject and set as child of the area collider
                GameObject ingredientInstance = Instantiate(area.ingredientData.prefab, randomPosition, Quaternion.identity);
                ingredientInstance.transform.SetParent(area.areaCollider.transform);
            }
        }
    }

    Vector3 GetValidSpawnPoint(Collider areaCollider)
    {
        Vector3 point;
        bool isValid;

        // Repeat until we find a valid point that is not overlapping with any other ingredients
        do
        {
            point = GetRandomPointInCollider(areaCollider);
            isValid = !Physics.CheckSphere(point, spawnRadius, ingredientLayerMask);
        } while (!isValid);

        return point;
    }

    Vector3 GetRandomPointInCollider(Collider areaCollider)
    {
        Vector3 point;

        // Repeat until we find a valid point inside the collider bounds
        do
        {
            point = new Vector3(
                Random.Range(areaCollider.bounds.min.x, areaCollider.bounds.max.x),
                Random.Range(areaCollider.bounds.min.y, areaCollider.bounds.max.y),
                Random.Range(areaCollider.bounds.min.z, areaCollider.bounds.max.z)
            );
        } while (!areaCollider.bounds.Contains(point));

        // Apply a small offset above the ground to ensure the object doesn't clip through
        point.y = Mathf.Max(point.y, areaCollider.bounds.min.y + 0.25f); // Safely set Y-position above ground

        return point;
    }
}
