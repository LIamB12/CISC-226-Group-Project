using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float expirationTimer;
    [SerializeField] private float damage;
    [HideInInspector] public int direction;
    [HideInInspector] public Player owner;

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(8.5f * owner.facingDirection, 0);
        
        expirationTimer -= Time.fixedDeltaTime;

        if (expirationTimer <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.gameObject != owner.gameObject)
        {
            Player collidingPlayer = collision.gameObject.GetComponent<Player>();
            collidingPlayer.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
