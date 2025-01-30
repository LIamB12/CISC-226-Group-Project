using UnityEngine;

public class Wizard : Player
{
    [Space]
    [Header("Subclass Properties")]
    [SerializeField] protected GameObject AbilityPrefab;

    
    protected override void UseAbility()
    {
        GameObject newProjectile = Instantiate(AbilityPrefab, (Vector2)this.transform.position + new Vector2(1.5f * facingDirection, 0), Quaternion.identity);
        newProjectile.GetComponent<Fireball>().owner = this;
        newProjectile.GetComponent<Fireball>().direction = facingDirection;
        abilityCooldownTime = abilityCooldownMaxTime;
        
    }
}
