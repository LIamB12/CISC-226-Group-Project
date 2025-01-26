using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float scrollSpeed = 3f; // Speed at which the camera moves upward

    void FixedUpdate()
    {
        // Move the camera upward at a consistent speed
        transform.position += Vector3.up * scrollSpeed * Time.fixedDeltaTime;
    }
}
