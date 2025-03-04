using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float expirationTimer;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [HideInInspector] public int direction;
    [SerializeField] private float knockback;
    [HideInInspector] public Player owner;

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(speed * direction, 0);
        
        expirationTimer -= Time.fixedDeltaTime;

        if (expirationTimer <= 0)
            Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.gameObject != owner.gameObject)
        {

            Player collidingPlayer = collision.gameObject.GetComponent<Player>();
            collidingPlayer.TakeDamage(damage);
            
            Vector2 direction = collision.transform.position - transform.position;

            direction.Normalize();
            collidingPlayer.rb.AddForce(direction * knockback, ForceMode2D.Impulse);
            
            Destroy(gameObject);
            
            
        }
    }
}
