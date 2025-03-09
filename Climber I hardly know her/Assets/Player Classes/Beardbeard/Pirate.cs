using Unity.Mathematics;
using UnityEngine;

public class Pirate : Player
{
    [Space]
    [Header("Subclass Properties")]
    [SerializeField] protected GameObject AbilityPrefab;
    [SerializeField] public int chargeTime;
    [SerializeField] public float chargePower;

    // represents state of ability,
    // charging = grounded, holding ability
    // launched = ability released
    // grounded = grounded, not holding ability
    // notgrounded = not grounded, not holding ability
    private enum abilityState{charging, launched, grounded, notGrounded}

    private abilityState currentState = abilityState.notGrounded;

    [SerializeField] public GameObject anchorSwing;

    private new void Start()
    {
        base.Start();

        anchorSwing.SetActive(false);
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        // controls the buildup of anchor 'charge'
        if (isGrounded && Input.GetKey(key_Ability)){ // must be grounded to build 'charge'
            chargeTime ++; // time spent holding button
            chargePower = chargePower + 1 / math.log(chargeTime + 1); // charge power grows with diminishing returns
        }


        if (isGrounded){ // must be grounded to build 'charge'
            anchorSwing.SetActive(false);

            if (Input.GetKey(key_Ability)){
                anchorSwing.SetActive(true);
                chargeTime ++; // time spend holding button
                chargePower = chargePower + 1 / math.log(chargeTime + 1); // charge power grows with diminishing returns
            }
            else if(chargeTime > 0){
                anchorSwing.SetActive(true);
                clampVelocity = false;
                rb.linearVelocity = new Vector2(facingDirection*chargePower*2, chargePower*2); // apply force
                
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
