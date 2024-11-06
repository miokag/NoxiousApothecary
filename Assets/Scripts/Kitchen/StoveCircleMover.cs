using UnityEngine;
using System.Collections.Generic;

public class StoveCircleMover : MonoBehaviour
{
    public RectTransform stoveClickerPanel; // The panel containing the sections
    public List<RectTransform> redSections;
    public List<RectTransform> orangeSections;
    public List<RectTransform> greenSections;

    public float moveSpeed = 200f;
    private RectTransform rectTransform;

    public KitchenManager kitchenManager;

    private float leftBound;
    private float rightBound;
    private float edgeOffset = 20f;
    private bool isMoving = true; // Start as true to enable movement immediately

    private float startTime;

    public void StartMoving()
    {
        isMoving = true; // Starts the movement
        startTime = Time.time; // Reset start time to allow smooth movement
    }

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Calculate movement bounds within the stoveClickerPanel
        float panelWidth = stoveClickerPanel.rect.width;
        leftBound = -panelWidth / 2 + edgeOffset;
        rightBound = panelWidth / 2 - edgeOffset;

        startTime = Time.time; // Track the start time

        // Set the initial position at the left bound
        rectTransform.anchoredPosition = new Vector2(leftBound, rectTransform.anchoredPosition.y);
        isMoving = true; // Ensure movement is active
        kitchenManager = GameObject.Find("KitchenManager").GetComponent<KitchenManager>();
    }

    private void Update()
    {
        // If movement is enabled, continue moving
        if (isMoving)
        {
            MoveCircle();
        }

        // Toggle movement on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = !isMoving;
            if (!isMoving)
            {
                CheckStoppedColor(); // Check color only when movement stops
                Destroy(stoveClickerPanel.gameObject); // Destroy the stoveClickerPanel
                kitchenManager.CheckClickedObject(); // Start the mini-game after destruction
            }
        }
    }

    private void MoveCircle()
    {
        // Move the circle back and forth within the specified bounds
        float elapsedTime = (Time.time - startTime) * moveSpeed;
        float newXPosition = Mathf.PingPong(elapsedTime, rightBound - leftBound) + leftBound;
        rectTransform.anchoredPosition = new Vector2(newXPosition, rectTransform.anchoredPosition.y);
    }

    public void CheckStoppedColor()
    {
        Vector2 circlePosition = rectTransform.anchoredPosition;
        Debug.Log($"Circle position: {circlePosition}");

        if (kitchenManager == null)
        {
            Debug.LogError("kitchenManager is not assigned!");
            return;
        }

        bool isOverlapping = false;

        // Check if the circle is overlapping with any section
        if (IsOverlappingWithAny(circlePosition, redSections))
        {
            Debug.Log("Circle stopped on Red section!");
            isOverlapping = true;
            kitchenManager.SetOverlappedColor("Red");
        }
        else if (IsOverlappingWithAny(circlePosition, orangeSections))
        {
            Debug.Log("Circle stopped on Orange section!");
            isOverlapping = true;
            kitchenManager.SetOverlappedColor("Orange");
        }
        else if (IsOverlappingWithAny(circlePosition, greenSections))
        {
            Debug.Log("Circle stopped on Green section!");
            isOverlapping = true;
            kitchenManager.SetOverlappedColor("Green");
        }

        if (!isOverlapping)
        {
            Debug.Log("Circle is outside of the color sections.");
            kitchenManager.SetOverlappedColor("");  // No color selected
        }
    }


    private bool IsOverlappingWithAny(Vector2 position, List<RectTransform> sections)
    {
        Vector3 worldPosition = stoveClickerPanel.TransformPoint(position);

        foreach (var section in sections)
        {
            Vector3[] corners = new Vector3[4];
            section.GetWorldCorners(corners);

            if (worldPosition.x >= corners[0].x && worldPosition.x <= corners[2].x &&
                worldPosition.y >= corners[0].y && worldPosition.y <= corners[1].y)
            {
                Debug.Log($"Circle overlaps with {section.name} at position: {worldPosition}");
                return true;
            }
        }
        return false;
    }
}
