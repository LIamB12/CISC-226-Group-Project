using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    public GameObject platformPrefab; // Reference to the platform prefab
    public float spawnRangeX = 4f;    // Horizontal range for spawning platforms
    public float spawnHeight = 5f;   // Vertical distance between platforms
    public float initialSpawnY = 0f; // Starting height for spawning platforms

    public float nextSpawnY;        // Tracks the next height for platform spawn

    void Start()
    {
        // Set the initial spawn position
        nextSpawnY = initialSpawnY;
    }

    void FixedUpdate()
    {
        // Check if the camera has moved past the next spawn height
        if (Camera.main.transform.position.y > nextSpawnY - spawnHeight)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // Generate a random X position within the range
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);

        // Spawn the platform at the determined position
        Vector3 spawnPosition = new Vector3(spawnX, nextSpawnY, 0f);
        Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Update the next spawn height
        nextSpawnY += spawnHeight;
    }
}

