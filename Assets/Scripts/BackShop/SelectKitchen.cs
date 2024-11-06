using UnityEngine;

public class SelectKitchen : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    private Color hoverColor = Color.yellow;
    public bool canInteract = true; // Make sure canInteract is set to true initially
    public SceneChanger changer;

    private void Start()
    {
        rend = GetComponent<Renderer>();

        // Check if renderer is successfully found
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    void OnMouseEnter()
    {
        if (canInteract && rend != null)
        {
            rend.material.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        if (canInteract && rend != null)
        {
            rend.material.color = originalColor;
        }
    }

    private void OnMouseDown()
    {
        if (canInteract && rend != null)
        {
            canInteract = false;
            rend.material.color = originalColor; // Reset color just in case
            changer.ToKitchenScene();
        }
    }
}
