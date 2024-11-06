using UnityEngine;

public class KitchenManager : MonoBehaviour
{
    // Reference to Fry and Boil GameObjects (these should be assigned in the inspector)
    public GameObject fryGameObject;
    public GameObject boilGameObject;

    // References to the mini-game scripts (assign these in the Inspector)
    public GameObject fryingMiniGame;
    public GameObject boilingMiniGame;

    public CameraController cameraController;


    public string overlappedColor = "";

    public void CheckClickedObject()
    {
        string focusPoint = cameraController.stoveFocusPoint.name;
        string fryObject = fryGameObject.name;
        string boilObject = boilGameObject.name;
        Debug.Log("CheckClickedObject: " + focusPoint);
        if (focusPoint == fryObject)
        {
          StartFryingGame();
        }
        else if (focusPoint == boilObject)
        {
          StartBoilingGame();
        }
        else
        {
          Debug.Log("Clicked on a different object.");
        }
    }

    // Method to start the frying mini-game
    private void StartFryingGame()
    {
        Debug.Log("Starting Frying Game...");

        Instantiate(fryingMiniGame);
    }

    // Method to start the boiling mini-game
    private void StartBoilingGame()
    {
        Debug.Log("Starting Frying Game...");

        Instantiate(boilingMiniGame);
    }

    // Method to update the overlapped color
    public void SetOverlappedColor(string color)
    {
        overlappedColor = color;
        Debug.Log($"Overlapped Color Set to: {color}");
    }

    // You can use this method to retrieve the overlapped color
    public string GetOverlappedColor()
    {
        return overlappedColor;
    }
}
