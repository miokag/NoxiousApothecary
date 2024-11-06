using UnityEngine;

public class RhythmSceneManager : MonoBehaviour
{
    public GameObject opiumPoppyTreeSpawnerPrefab; // Assign this in the Inspector
    private GameObject noteSpawnerInstance;

    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }

        if (GameManager.Instance.SelectedIngredient != null)
        {
            Ingredient selectedIngredient = GameManager.Instance.SelectedIngredient;
            Debug.Log($"Selected Ingredient: {selectedIngredient.ingredientName}");

            // Check for specific ingredients
            if (selectedIngredient.ingredientName == "Opium Poppy Tree")
            {
                // Instantiate the Opium Poppy Tree spawner prefab
                noteSpawnerInstance = Instantiate(opiumPoppyTreeSpawnerPrefab, transform.position, Quaternion.identity);
            }
            else if (selectedIngredient.ingredientName == "Gale Fern Fronds")
            {
                // Instantiate the Opium Poppy Tree spawner prefab
                noteSpawnerInstance = Instantiate(opiumPoppyTreeSpawnerPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No specific spawner behavior defined for this ingredient.");
            }
        }
        else
        {
            Debug.LogWarning("No ingredient data found in GameManager!");
        }
    }
}
