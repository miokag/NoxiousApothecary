using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Collider spawnCollider;
    public float spawnInterval = 1.0f;
    public float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnNote();
            timer = 0f;
        }
    }

    void SpawnNote()
    {
        Vector3 spawnPosition = GetRandomPointInCollider(spawnCollider);
        GameObject newNote = Instantiate(notePrefab, spawnPosition, Quaternion.identity);
    }

    public Vector3 GetRandomPointInCollider(Collider collider)
    {
        // Get the bounds of the collider
        Bounds bounds = collider.bounds;

        // Generate a random point within the bounds
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}