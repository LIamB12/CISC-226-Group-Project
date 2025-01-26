using System;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
    [SerializeField] private float expirationTimer;
    [SerializeField] private float damage;
    [HideInInspector] public GameObject owner;

    void FixedUpdate()
    {
        expirationTimer -= Time.fixedDeltaTime;

        if (expirationTimer <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.gameObject != owner)
        {
            Player collidingPlayer = collision.gameObject.GetComponent<Player>();
            collidingPlayer.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
