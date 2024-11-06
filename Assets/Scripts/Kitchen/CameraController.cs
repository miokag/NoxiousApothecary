using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform stoveFocusPoint; // The current stove focus point
    public float moveDuration = 1.5f;
    public float targetFieldOfView = 30f;
    private float originalFieldOfView;
    private Vector3 originalPosition;
    public float FocusDuration = 10f;

    public float focusOffset = 1.5f; // Adjust this for the desired distance

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        originalFieldOfView = mainCamera.fieldOfView;
        originalPosition = mainCamera.transform.position;
    }

    // Method to update the stove focus point dynamically
    public void SetStoveFocusPoint(Transform newFocusPoint)
    {
        stoveFocusPoint = newFocusPoint; // Set the new focus point
        Debug.Log("Stove focus point updated to: " + stoveFocusPoint.name);
    }

    public void StartFocusOnStove()
    {
        if (stoveFocusPoint != null)
        {
            StartCoroutine(FocusOnStove());
        }
        else
        {
            Debug.LogWarning("Stove focus point is not set!");
        }
    }

    private IEnumerator FocusOnStove()
    {
        float elapsedTime = 0f;
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;
        float startFOV = mainCamera.fieldOfView;

        // Calculate target position with offset
        Vector3 targetPosition = stoveFocusPoint.position - mainCamera.transform.forward * focusOffset;
        Quaternion targetRotation = Quaternion.LookRotation(stoveFocusPoint.position - targetPosition);

        while (elapsedTime < moveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPosition, elapsedTime / moveDuration);
            mainCamera.transform.rotation = Quaternion.Slerp(startRot, targetRotation, elapsedTime / moveDuration);
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, targetFieldOfView, elapsedTime / moveDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;
        mainCamera.fieldOfView = targetFieldOfView;
    }

    public void ResetCamera()
    {
        mainCamera.transform.position = originalPosition;
        mainCamera.fieldOfView = originalFieldOfView;
    }
}
