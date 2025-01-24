using System;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
    [SerializeField] private float ExpirationTimer;
    [SerializeField] private float Damage;
    [HideInInspector] public GameObject owner;

    void FixedUpdate()
    {
        ExpirationTimer -= Time.fixedDeltaTime;

        if (ExpirationTimer <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.gameObject != owner)
        {
            Player collidingPlayer = collision.gameObject.GetComponent<Player>();
            collidingPlayer.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
