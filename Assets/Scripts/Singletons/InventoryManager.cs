using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public GameObject inventoryPanelPrefab; // Reference to the Inventory Panel container prefab
    public GameObject inventoryInnerPanelPrefab; // Reference to the Inner Panel prefab for each item
    private GameObject inventoryPanel; // Instance of the Inventory Panel container
    private List<string> items = new List<string>(); // List to store item names
    private const int maxInventorySize = 3; // Maximum size of the inventory
    public GameObject warningPrefab; // Reference to the warning prefab
    private GameObject activeWarning; // Store the active warning instance


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }

    void Start()
    {
        // Automatically find and instantiate the inventory panel only in the exploration scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Exploration")
        {
            SetupInventoryPanel();
        }
    }

    private void SetupInventoryPanel()
    {
        if (inventoryPanel == null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                inventoryPanel = Instantiate(inventoryPanelPrefab, canvas.transform);
                inventoryPanel.SetActive(true);

                Transform layoutGroupTransform = inventoryPanel.transform.Find("HorizontalLayoutGroup");
                if (layoutGroupTransform != null)
                {
                    for (int i = 0; i < maxInventorySize; i++)
                    {
                        GameObject innerPanel = Instantiate(inventoryInnerPanelPrefab, layoutGroupTransform);
                        innerPanel.name = $"InnerPanel_{i}";

                        Transform childInnerPanelTransform = innerPanel.transform.Find("InnerPanel");
                        if (childInnerPanelTransform != null)
                        {
                            Transform imageTransform = childInnerPanelTransform.Find("Image");
                            if (imageTransform != null)
                            {
                                imageTransform.gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError("HorizontalLayoutGroup not found in the inventory panel!");
                }
            }
            else
            {
                Debug.LogError("Canvas not found in the scene!");
            }
        }
    }

    public bool AddItem(string itemName)
    {
        if (!IsInventoryFull())
        {
            items.Add(itemName);
            Debug.Log($"Added {itemName} to inventory.");
            UpdateInventoryUI();
            return true;
        }
        else
        {
            Debug.LogWarning("Inventory is full! Cannot add item: " + itemName);
            return false;
        }
    }

    public void ShowInventoryFullWarning()
    {
        if (activeWarning == null && warningPrefab != null)
        {
            // Find the Canvas to make sure the warning shows up in the correct location
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                activeWarning = Instantiate(warningPrefab, canvas.transform);
                activeWarning.SetActive(true); // Ensure the warning is active

                // Position the warning at the center of the screen
                RectTransform warningRectTransform = activeWarning.GetComponent<RectTransform>();
                if (warningRectTransform != null)
                {
                    warningRectTransform.anchoredPosition = Vector2.zero; // Center it
                }

                // Start coroutine to destroy the warning after a few seconds
                StartCoroutine(DestroyWarningAfterDelay(3f)); // Destroy after 3 seconds
            }
            else
            {
                Debug.LogError("Canvas not found, cannot display inventory full warning!");
            }
        }
    }

    // Coroutine to destroy the warning after a delay
    private IEnumerator DestroyWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (activeWarning != null)
        {
            Destroy(activeWarning);
            activeWarning = null; // Clear the reference
        }
    }


    public void GoToRhythmScene()
    {
        // Only change scenes if the inventory is not full
        if (!IsInventoryFull())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Rhythm");
        }
        else
        {
            ShowInventoryFullWarning();
        }
    }

    public void UpdateInventoryUI()
    {
        if (inventoryPanel == null)
        {
            Debug.LogWarning("Inventory panel is null, cannot update UI.");
            return;
        }

        for (int i = 0; i < maxInventorySize; i++)
        {
            Transform innerPanelTransform = inventoryPanel.transform.Find("HorizontalLayoutGroup/InnerPanel_" + i + "/InnerPanel");
            if (innerPanelTransform != null)
            {
                Transform imageTransform = innerPanelTransform.Find("Image");
                if (imageTransform != null)
                {
                    imageTransform.gameObject.SetActive(false);
                }
            }
        }

        for (int i = 0; i < items.Count && i < maxInventorySize; i++)
        {
            string itemName = items[i];
            Ingredient ingredient = IngredientList.Instance.GetIngredientByName(itemName);
            if (ingredient != null)
            {
                Transform innerPanelTransform = inventoryPanel.transform.Find("HorizontalLayoutGroup/InnerPanel_" + i + "/InnerPanel");
                if (innerPanelTransform != null)
                {
                    Transform imageTransform = innerPanelTransform.Find("Image");
                    if (imageTransform != null)
                    {
                        Image iconImage = imageTransform.GetComponent<Image>();
                        if (iconImage != null)
                        {
                            iconImage.sprite = ingredient.icon;
                            imageTransform.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    public bool IsInventoryFull()
    {
        return items.Count >= maxInventorySize;
    }

    public List<string> GetAllItems()
    {
        return new List<string>(items);
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name == "Exploration")
        {
            SetupInventoryPanel();
            UpdateInventoryUI();
        }

        else if (scene.name == "BackShop")
        {
            SetupInventoryPanel();
            UpdateInventoryUI();
        }

        else if (scene.name == "Kitchen")
        {
            SetupInventoryPanel();
            UpdateInventoryUI();
        }
    }
}
