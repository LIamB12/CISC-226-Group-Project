using System;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] private Vector2 Direction;

    private void FixedUpdate()
    {
        transform.Translate(Direction * Time.fixedDeltaTime);
        
    }
}
