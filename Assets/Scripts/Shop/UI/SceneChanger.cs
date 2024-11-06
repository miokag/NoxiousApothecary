using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class SceneChanger : MonoBehaviour
{
    public void ToBackShopScene()
    {
        // Directly load the scene by name
        SceneManager.LoadScene("BackShop");
    }

    public void ToKitchenScene()
    {
        // Directly load the scene by name
        SceneManager.LoadScene("Kitchen");
    }

    public void ToExplorationScene()
    {
        // Directly load the scene by name
        SceneManager.LoadScene("Exploration");
    }

    public void ToFrontShopScene()
    {
        // Directly load the scene by name
        SceneManager.LoadScene("Shop");
    }
}
