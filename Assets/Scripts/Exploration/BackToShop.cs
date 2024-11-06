using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToShop : MonoBehaviour
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
        SceneManager.LoadScene("BackShop");
        Cursor.lockState = CursorLockMode.None;
    }
}
