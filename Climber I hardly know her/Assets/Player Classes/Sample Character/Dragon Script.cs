using UnityEngine;

public class DragonScript : Player
{
    [Space]
    [Header("Subclass Properties")]
    [SerializeField] protected GameObject AbilityPrefab;

    
    protected override void UseAbility()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 20);
        abilityCooldownTime = abilityCooldownMaxTime;
    }
}
