using UnityEngine;

public class BackgroundDuplication : MonoBehaviour
{
    public GameObject canvas; // Reference to the canvas object
    public float imageHeight = 5f;   // Vertical distance between platforms
    public float initialSpawnY = 0f; 
    public float xPos;
    public float nextSpawnY;        // Tracks the next height for platform spawn
    public float zPos;

    void Start()
    {
        // Set the initial spawn position
        nextSpawnY = initialSpawnY;
    }

    void FixedUpdate()
    {
        // Check if the camera has moved past the next spawn height
        if (Camera.main.transform.position.y > nextSpawnY - imageHeight)
        {
            DuplicateImage();
        }
    }

    void DuplicateImage()
    {
        // Generate a random X position within the range

        // Spawn the platform at the determined position
        Vector3 spawnPosition = new Vector3(xPos, nextSpawnY, zPos);
        Instantiate(canvas, spawnPosition, Quaternion.identity);

        // Update the next spawn height
        nextSpawnY += imageHeight;
    }
}

