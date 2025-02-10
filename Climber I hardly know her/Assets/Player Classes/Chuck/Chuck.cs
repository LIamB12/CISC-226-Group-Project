using UnityEngine;

public class Chuck : Player
{
    //[Space]
    //[Header("Subclass Properties")]
    [SerializeField] protected GameObject HoldPos;
    [SerializeField] protected float abilityFailureMaxTime = 1;
    [SerializeField] protected float abilityFailureTime = -1;

    private RaycastHit hit;
    protected override void UseAbility()
    {
        /*
        Vector2 origin = (Vector2)transform.position + new Vector2(GetComponent<BoxCollider2D>().size.x / 1.5f * facingDirection, 0);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * facingDirection, 1f);
        Debug.DrawRay(origin, Vector2.right * facingDirection * 1f, Color.red, 1f);

        if (!hit)
        {
            if (abilityFailureTime < 0)
                abilityFailureTime = 0;
            
            return;
        }

        Debug.Log(hit.transform.gameObject.name);

        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = hit.transform.GetComponent<Player>();
            Debug.Log(hit.transform.gameObject.name);
            player.rb.simulated = false;
            player.transform.parent = HoldPos.transform;
            player.transform.localPosition = Vector2.zero + new Vector2(0, GetComponent<BoxCollider2D>().size.x / 2);
        }*/

    }

    private void FixedUpdate()
    { /*
        if (abilityFailureTime < abilityFailureMaxTime && abilityFailureTime >= 0)
        {
            abilityFailureTime += Time.fixedDeltaTime;
        }
        
        if(abilityFailureTime >= abilityFailureMaxTime)
            abilityCooldownTime = abilityCooldownMaxTime;*/
    }
}
