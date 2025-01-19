using UnityEngine;

public class Projectile : MonoBehaviour 
{
    [SerializeField] private float Timer;

    void FixedUpdate()
    {
        Timer -= Time.fixedDeltaTime;

        if (Timer <= 0)
            Destroy(gameObject);
    }
}
