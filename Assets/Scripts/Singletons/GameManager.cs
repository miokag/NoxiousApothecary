using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Ingredient SelectedIngredient { get; set; } // Field to hold the selected ingredient

    private float score;

    void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            Instance = this; // Set the instance to this
            DontDestroyOnLoad(gameObject); // Keep this instance alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate GameManagers
        }
    }
}
