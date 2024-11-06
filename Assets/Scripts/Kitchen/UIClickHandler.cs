using UnityEngine;
using System.Collections;

public class UIClickHandler : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject uiElementPrefab; // Prefab to instantiate (stove clicker panel)
    public GameObject backButtonPrefab; // Prefab for the back button
    public Canvas canvas; // Reference to the Canvas where the UI element will be instantiated
    public StoveCircleMover stoveCircleMover; // Reference to the StoveCircleMover for starting the circle movement
    public float delayTime = 0.5f; // Delay time in seconds before instantiating the UI element

    private static bool isStoveFocused = false;  // Flag to track if the stove focus is active
    private Renderer objectRenderer;

    private void Awake()
    {
        if (cameraController == null)
        {
            cameraController = FindObjectOfType<CameraController>();
        }

        if (canvas == null)
        {
            // Attempt to find the Canvas in the scene if not assigned
            canvas = FindObjectOfType<Canvas>();
        }

        if (stoveCircleMover == null)
        {
            stoveCircleMover = FindObjectOfType<StoveCircleMover>();
        }

        // Store a reference to the object's renderer to visually show if it is interactable
        objectRenderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        // If the stove focus is active, do nothing
        if (isStoveFocused)
        {
            return;
        }

        // Set this GameObject as the Stove Focus Point for the camera controller
        if (cameraController != null)
        {
            cameraController.SetStoveFocusPoint(this.transform);
            cameraController.StartFocusOnStove();
            isStoveFocused = true;  // Set the flag to true to indicate stove is focused
        }
        else
        {
            Debug.LogWarning("CameraController is not assigned.");
        }

        // Start the delay before instantiating the UI element
        StartCoroutine(InstantiateUIElementWithDelay());
    }

    private IEnumerator InstantiateUIElementWithDelay()
    {
        // Instantiate the back button
        if (backButtonPrefab != null && canvas != null)
        {
            GameObject backButton = Instantiate(backButtonPrefab, canvas.transform);
            BackButtonHandler backButtonHandler = backButton.GetComponent<BackButtonHandler>();

            if (backButtonHandler != null)
            {
                // Optionally, link the camera controller to the back button
                backButtonHandler.cameraController = cameraController;
            }
            else
            {
                Debug.LogWarning("BackButtonHandler script is not found on the back button prefab.");
            }
        }
        else
        {
            Debug.LogWarning("Back Button Prefab or Canvas is not assigned.");
        }
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Instantiate the stove clicker panel (stove circle mover and related UI)
        if (uiElementPrefab != null && canvas != null)
        {
            // Instantiate the stove clicker panel
            GameObject newUIElement = Instantiate(uiElementPrefab, canvas.transform);

            // You can access the StoveCircleMover on the instantiated object to modify its properties
            StoveCircleMover newStoveCircleMover = newUIElement.GetComponent<StoveCircleMover>();
            if (newStoveCircleMover != null)
            {
                // Additional setup for the StoveCircleMover can go here
            }
        }
        else
        {
            Debug.LogWarning("UI Element Prefab or Canvas is not assigned.");
        }

        
    }

    // Call this method to reset the stove focus point when the interaction is complete
    public static void ClearStoveFocus()
    {
        isStoveFocused = false;
    }

}
