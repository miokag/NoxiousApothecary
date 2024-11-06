using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
    public CameraController cameraController; // Reference to the CameraController

    // This will be called when the back button is clicked
    public void OnBackButtonClicked()
    {

         cameraController.ResetCamera(); // Clear the stove focus in the CameraController

        // Clear the stove focus (reset the static flag in UIClickHandler)
        UIClickHandler.ClearStoveFocus();

        // Optionally, you can also destroy the instantiated UI element if necessary
        Destroy(gameObject); // Destroys the back button or you can destroy other UI elements
    }
}
