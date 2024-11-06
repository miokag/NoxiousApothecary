using UnityEngine;
using TMPro;

public class OrderHandler : MonoBehaviour
{
    public GameObject dialogueBoxPrefab;
    public GameObject orderNotePrefab;
    public GameObject sceneChangePrefab;
    public GameObject dialogueBox;
    public OrderManager orderManager;
    private Canvas uiCanvas;

    private void Start()
    {
        uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void TakeOrder(CustomerActions customerActions)
    {
        // Get customer name and order description
        string customerName = customerActions.customerName;
        string orderDescription = customerActions.GetOrderDescription();
        PotionInfo potionOrder = customerActions.GetPotionOrder();

        // Add order to OrderManager
        OrderManager.Instance.AddOrder(customerName, orderDescription, potionOrder);

        // Instantiate the dialogue box prefab
        dialogueBox = Instantiate(dialogueBoxPrefab, uiCanvas.transform);
        RectTransform rectTransform = dialogueBox.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, -70);

        TMP_Text dialogueText = dialogueBox.GetComponentInChildren<TMP_Text>();
        string customerDialogue = customerActions.GetCustomerDialogue();
        if (dialogueText != null)
        {
            dialogueText.text = customerDialogue;
        }
    }

    public void InstantiateOrderNote(string orderDescription)
    {
        // Instantiate the order note prefab
        GameObject orderNote = Instantiate(orderNotePrefab, uiCanvas.transform);
        RectTransform orderNoteRectTransform = orderNote.GetComponent<RectTransform>();

        TMP_Text orderNoteText = orderNote.GetComponentInChildren<TMP_Text>();
        if (orderNoteText != null)
        {
            orderNoteText.text = orderDescription;
        }

        // Attach the OrderNoteInteractions component and set the OrderHandler reference
        OrderNoteInteractions orderNoteInteractions = orderNote.AddComponent<OrderNoteInteractions>();
        orderNoteInteractions.SetOrderHandler(this); // Pass the OrderHandler reference
    }

    public void ShowSceneChangeButton()
    {
        // Instantiate the scene change button prefab
        GameObject button = Instantiate(sceneChangePrefab, uiCanvas.transform);
    }
}
