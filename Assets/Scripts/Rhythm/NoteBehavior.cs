using System.Collections;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public GameObject ringPrefab; // Assign your ring prefab in the inspector
    private GameObject ringInstance;
    public float shrinkDuration = 1.0f; // Time in seconds for the ring to shrink
    public float hitThreshold = 0.1f; // Allowable size difference for a "hit"

    void Start()
    {
        if (ringPrefab != null)
        {
            // Instantiate ring without making it a child to avoid inherited scaling issues
            ringInstance = Instantiate(ringPrefab, transform.position, Quaternion.identity);
            ringInstance.transform.localScale = ringPrefab.transform.localScale; // Ensure it keeps its original scale

            // Start the shrinking coroutine
            StartCoroutine(ShrinkRing());
        }
    }

    void OnMouseDown()
    {
        // Calculate the size difference between the ring and the note
        float sizeDifference = Mathf.Abs(ringInstance.transform.localScale.x - transform.localScale.x);

        if (sizeDifference <= hitThreshold)
        {
            Debug.Log("Hit! Perfect timing.");
            // You can add points to the score here if desired
        }
        else
        {
            Debug.Log("Miss! Try again.");
        }

        // Handle scoring or other behavior on click
        Destroy(ringInstance); // Destroy the ring when the note is clicked
        Destroy(gameObject);   // Destroy the note itself
    }

    IEnumerator ShrinkRing()
    {
        Vector3 initialScale = ringInstance.transform.localScale;
        Vector3 targetScale = Vector3.zero;

        float elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            ringInstance.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / shrinkDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the ring reaches the target scale exactly
        ringInstance.transform.localScale = targetScale;

        // Destroy the ring and note immediately after shrinking is complete
        Destroy(ringInstance);
        Destroy(gameObject);
    }

}