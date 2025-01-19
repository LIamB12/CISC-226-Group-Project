using UnityEngine;

public class MyCharacterScript : Player
{
    public override void UseAbility()
    {
        GameObject newProjectile = Instantiate(projectile, (Vector2)this.transform.position + new Vector2(1.5f * facingDirection, 0), Quaternion.identity);
        abilityCooldownCounter = abilityCooldown;
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(8.5f * facingDirection, 5);

    }
}
