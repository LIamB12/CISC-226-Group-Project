using Unity.Mathematics;
using UnityEngine;

public class Pirate : Player
{
    [Space]
    [Header("Subclass Properties")]
    [SerializeField] protected GameObject AbilityPrefab;
    [SerializeField] public int chargeTime;
    [SerializeField] public float chargePower;

    /*protected override void UseAbility()
    {}*/

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        if (isGrounded){ // must be grounded to build 'charge'

            if (Input.GetKey(key_Ability)){
                chargeTime ++; // time spend holding button
                chargePower = chargePower + 1 / math.log(chargeTime + 1); // charge power grows with diminishing returns
            }
            else if(chargeTime > 0){
                isGrounded = false; // makes "J" launch better but doesnt fix it
                rb.linearVelocity = new Vector2(facingDirection*chargePower*4, chargePower*2); // apply force
                
                //reset charge values
                chargeTime = 0;
                chargePower = 0;
            }
        }
        else{
            //reset charge values when not grounded
            chargeTime = 0;
            chargePower = 0;
        }

    }
}
