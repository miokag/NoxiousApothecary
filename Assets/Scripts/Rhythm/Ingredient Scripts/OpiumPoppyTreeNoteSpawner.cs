using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class OpiumPoppyTreeNoteSpawner : MonoBehaviour
{
    public GameObject notePrefab; // Assign your note prefab in the Inspector
    public float spawnInterval = 1.0f; // Interval for non-ghost notes
    public float ghostSpawnInterval = 0.5f; // Interval for ghost notes
    public float ghostNoteOpacity = 0.3f; // Opacity for ghost notes
    public float minSpawnDistance = 0.5f; // Minimum distance between notes
    private int totalNotes; // Track total non-ghost notes spawned
    private int maxNotes; // Maximum notes allowed
    private Collider spawnCollider; // To hold the NoteRange collider
    private List<Vector3> occupiedPositions = new List<Vector3>(); // To track occupied positions
    private List<GameObject> nonGhostNotes = new List<GameObject>(); // List to track non-ghost notes

    void Start()
    {
        // Find the NoteRange collider in the scene
        GameObject noteRangeObject = GameObject.Find("NoteRange");
        if (noteRangeObject != null)
        {
            spawnCollider = noteRangeObject.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("NoteRange object not found in the scene!");
            return; // Exit if the collider is not found
        }

        // Set a random number between 10 and 15 for the total notes
        maxNotes = Random.Range(3, 5);
        totalNotes = 0;

        Debug.Log($"Max Notes Allowed: {maxNotes}"); // Log max notes

        // Start the spawning process
        StartCoroutine(SpawnNotes());
    }

    private IEnumerator SpawnNotes()
    {
        while (totalNotes < maxNotes)
        {
            SpawnNonGhostNote();
            yield return new WaitForSeconds(spawnInterval);

            SpawnGhostNotes();
            yield return new WaitForSeconds(ghostSpawnInterval);
        }

        // After spawning all notes, check if all non-ghost notes are destroyed
        yield return StartCoroutine(CheckForNotesCompletion());
    }


    void SpawnNonGhostNote()
    {
        Vector3 spawnPosition = GetValidSpawnPosition();
        if (spawnPosition != Vector3.zero)
        {
            GameObject newNote = Instantiate(notePrefab, spawnPosition, Quaternion.identity);
            SetNoteOpacity(newNote, 1.0f);
            occupiedPositions.Add(spawnPosition);
            nonGhostNotes.Add(newNote); // Add to non-ghost notes list
            totalNotes++;

            Debug.Log("Spawned non-ghost note. Total Notes: " + totalNotes);
        }
    }

    private IEnumerator CheckForNotesCompletion()
    {
        while (nonGhostNotes.Count > 0)
        {
            yield return new WaitForSeconds(0.5f); // Check every half second
            nonGhostNotes.RemoveAll(note => note == null); // Clean up destroyed notes
        }

        // Add the ingredient to the inventory
        if (GameManager.Instance != null && GameManager.Instance.SelectedIngredient != null)
        {
            string ingredientName = GameManager.Instance.SelectedIngredient.ingredientName;
            InventoryManager.Instance.AddItem(ingredientName);
            Debug.Log("Ingredient added to inventory: " + ingredientName);
        }
        else
        {
            Debug.LogWarning("Selected ingredient or GameManager instance is null.");
        }

        // Once all non-ghost notes are destroyed, return to the exploration scene
        SceneManager.LoadScene("Exploration"); // Replace with your exploration scene name
    }

    void SpawnGhostNotes()
    {
        // Randomly determine how many ghost notes to spawn (up to 5)
        int ghostNoteCount = Random.Range(1, 6);
        for (int i = 0; i < ghostNoteCount; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            if (spawnPosition != Vector3.zero) // Check if we found a valid position
            {
                GameObject ghostNote = Instantiate(notePrefab, spawnPosition, Quaternion.identity);
                SetNoteOpacity(ghostNote, ghostNoteOpacity);
                occupiedPositions.Add(spawnPosition); // Add the position to the occupied list for ghost notes

                Debug.Log("Spawned ghost note."); // Log ghost note spawn
            }
        }
    }

    public void NoteDestroyed(GameObject note)
    {
        // Remove the destroyed note from the list
        if (nonGhostNotes.Contains(note))
        {
            nonGhostNotes.Remove(note);
            Debug.Log("A non-ghost note was destroyed. Remaining non-ghost notes: " + nonGhostNotes.Count);

            // Check if all non-ghost notes have been destroyed
            if (nonGhostNotes.Count == 0)
            {
                EndRhythmGame(); // End the rhythm game and return to exploration
            }
        }
    }

    void EndRhythmGame()
    {
        // Logic to end the rhythm game and return to the exploration scene
        SceneManager.LoadScene("Exploration"); // Replace with the actual name of your exploration scene
    }

    Vector3 GetValidSpawnPosition()
    {
        for (int attempts = 0; attempts < 10; attempts++) // Limit the number of attempts to find a valid position
        {
            Vector3 position = GetRandomPointInCollider(spawnCollider);
            bool isValid = true;

            // Check the distance from all occupied positions
            foreach (Vector3 occupied in occupiedPositions)
            {
                if (Vector3.Distance(position, occupied) < minSpawnDistance)
                {
                    isValid = false; // Position is too close to an existing note
                    break;
                }
            }

            if (isValid)
            {
                return position; // Return valid spawn position
            }
        }

        return Vector3.zero; // Return zero if no valid position found after attempts
    }

    void SetNoteOpacity(GameObject note, float opacity)
    {
        // Set opacity for the main note
        SpriteRenderer renderer = note.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            Color color = renderer.color;
            color.a = opacity;
            renderer.color = color;
        }

        SpriteRenderer ringRenderer = note.GetComponentInChildren<SpriteRenderer>();
        if (ringRenderer != null)
        {
            Color ringColor = ringRenderer.color;
            ringColor.a = opacity;
            ringRenderer.color = ringColor; // Set the ring's opacity
        }
        else
        {
            Debug.LogError("Ring child not found in note prefab.");
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        if (collider == null) return Vector3.zero; // Prevent errors if collider is not found

        Bounds bounds = collider.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(randomX, randomY, bounds.center.z); // Adjust to only vary X and Y
    }
}
