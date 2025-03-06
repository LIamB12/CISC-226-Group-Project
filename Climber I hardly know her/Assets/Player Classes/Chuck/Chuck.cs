using System;
using UnityEngine;

public class Chuck : Player
{
    [Space]
    [Header("Subclass Properties")]
    [SerializeField] protected GameObject HoldPos;
    [SerializeField] protected float abilityFailureMaxTime = 1;
    [SerializeField] protected float abilityFailureTime = -1;
    [SerializeField] protected float ThrowForce = 10;
    [SerializeField] protected bool playerGrabbed = false;
    [SerializeField] protected GameObject GrabbedPlayer;

    private RaycastHit hit;
    protected override void UseAbility()
    {

        if (playerGrabbed)
            return;

        Vector2 origin = (Vector2) transform.position + new Vector2(GetComponent<BoxCollider2D>().size.x / 1.5f * facingDirection, 0);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * facingDirection, 1f);
        Debug.DrawRay(origin, Vector2.right * facingDirection * 1f, Color.red, 1f);

        if (!hit)
        {
            if (abilityFailureTime < 0)
                abilityFailureTime = 0;

            return;
        }
        
        
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            abilityFailureTime = -1;
            //abort failure timer, since we have succeeded in grabbing someone.

            GrabbedPlayer = hit.transform.gameObject;
            Player player = GrabbedPlayer.GetComponent<Player>();
            player.rb.simulated = false;
            player.transform.parent = HoldPos.transform;
            player.transform.localPosition = Vector2.zero + new Vector2(0, GetComponent<BoxCollider2D>().size.x / 2);
            playerGrabbed = true;
        }

    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();


        if (abilityFailureTime < abilityFailureMaxTime && abilityFailureTime >= 0)
        {
            abilityFailureTime += Time.fixedDeltaTime;
        }

        if (abilityFailureTime >= abilityFailureMaxTime)
        {
            abilityCooldownTime = abilityCooldownMaxTime;
            abilityFailureTime = -1;
        }

    }

    private void Update()
    {
        
        if (Input.GetKeyUp(key_Ability) && !playerGrabbed && abilityFailureTime > 0)
            abilityFailureTime = abilityFailureMaxTime;
        
        //if we let go of ability key and aren't holding someone and we are counting down, skip the failure timer, go straight to standard cooldown.
        
        if (Input.GetKeyUp(key_Ability) && playerGrabbed)
        {
            playerGrabbed = false;
            GrabbedPlayer.transform.parent = null;
            GrabbedPlayer.GetComponent<Rigidbody2D>().simulated = true;
            GrabbedPlayer.GetComponent<Rigidbody2D>().linearVelocity = rb.linearVelocity;
            GrabbedPlayer.GetComponent<Rigidbody2D>().linearVelocityX += ThrowForce * facingDirection;
            GrabbedPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(ThrowForce * facingDirection, 2), ForceMode2D.Impulse);
            Debug.Log(GrabbedPlayer.GetComponent<Rigidbody2D>().linearVelocity);

        }
    }
}
