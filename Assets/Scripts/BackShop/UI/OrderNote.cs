using UnityEngine;
using UnityEngine.UI; // Import the UI namespace
using UnityEngine.EventSystems; // Import the EventSystems namespace

public class OrderNote : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image img; // Use Image component for UI elements
    private Color originalColor;
    private Color hoverColor = Color.yellow;
    private OrderHandler orderHandler;
    private OrderStackHandler orderStackHandler; 
    public SceneChanger changer;

    public void SetOrderHandler(OrderHandler handler)
    {
        orderHandler = handler;
    }

    void Start()
    {
        img = GetComponent<Image>(); // Get the Image component
        if (img != null)
        {
            originalColor = img.color; // Get the original color
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (img != null)
        {
            img.color = hoverColor; // Change color on hover
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (img != null)
        {
            img.color = originalColor; // Reset color on exit
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
        changer.ToExplorationScene();
    }
}