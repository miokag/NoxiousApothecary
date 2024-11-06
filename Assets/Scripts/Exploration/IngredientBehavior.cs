using UnityEngine;
using UnityEngine.SceneManagement;

public class IngredientBehavior : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    private Color hoverColor = Color.yellow;

    public Ingredient ingredientData; // Reference to the Ingredient ScriptableObject

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = originalColor;
    }

    void OnMouseDown()
    {
        rend.material.color = originalColor;

        // Ensure ingredientData is not null before setting it in GameManager
        if (ingredientData != null)
        {
            // Check if the inventory is full
            if (InventoryManager.Instance != null && InventoryManager.Instance.IsInventoryFull())
            {
                Debug.LogWarning("Cannot load Rhythm scene. Inventory is full!");
                InventoryManager.Instance.ShowInventoryFullWarning();
                return; // Stop execution to prevent scene loading
            }

            // If inventory has space, set the selected ingredient and load the rhythm scene
            GameManager.Instance.SelectedIngredient = ingredientData;

            // Debug log to confirm the ingredient selection
            Debug.Log("Selected Ingredient: " + ingredientData.ingredientName);

            // Load the rhythm scene
            SceneManager.LoadScene("Rhythm");
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Debug.LogWarning("IngredientData is null for " + gameObject.name);
        }
    }
}
