using UnityEngine;

public class MyCharacterScript : Player
{
    [Space]
    [Header("Subclass Properties")]
    [SerializeField] protected GameObject AbilityPrefab;

    
    protected override void UseAbility()
    {
        GameObject newProjectile = Instantiate(AbilityPrefab, (Vector2)this.transform.position + new Vector2(1.5f * facingDirection, 0), Quaternion.identity);
        newProjectile.GetComponent<Projectile>().owner = gameObject;
        abilityCooldownTime = abilityCooldownMaxTime;
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(8.5f * facingDirection, 5);
        
    }
}
