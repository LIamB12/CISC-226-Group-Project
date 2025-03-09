using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;

public class Pirate : Player
{
    [Space]
    [Header("Subclass Properties")]
    [SerializeField] protected GameObject AbilityPrefab;
    [SerializeField] public int chargeTime;
    [SerializeField] public float chargePower;

    private float baseMoveSpeed;

    private float chargeingMoveSpeed;

    [SerializeField] private int speedReducedUnderAbility;



    // represents transition state of ability,
    // normal = base state, not interacting with ability
    // charging = grounded, holding ability
    // launched = ability released
    private enum abilityState{normal, charging, launched}

    private abilityState currentState = abilityState.normal;

    [SerializeField] public GameObject anchorSwing;

    

    private new void Start()
    {
        base.Start();

        anchorSwing.SetActive(false);
        baseMoveSpeed = moveSpeed;
        chargeingMoveSpeed = moveSpeed / speedReducedUnderAbility;

    }

    /*
    Valid ability state transitions:
    (charging)-------------->(launched)
      |    A                     |
      |    |                     |
      V    |                     |
    ( normal ) <-----------------/
    */
    private void manageAbilityState()
    {
        switch (currentState)
        {
            case abilityState.normal:
                // transition to charging state
                if (isGrounded && Input.GetKey(key_Ability)){
                    currentState = abilityState.charging;
                    anchorSwing.SetActive(true);
                    moveSpeed = chargeingMoveSpeed;
                }
                break;

            case abilityState.charging:
                // transition to launched state
                if (chargeTime > 0 && !Input.GetKey(key_Ability)){
                    currentState = abilityState.launched;
                    anchorSwing.SetActive(true);
                    clampVelocity = false;
                    rb.linearVelocity = new Vector2(facingDirection*chargePower*2, chargePower*3); // apply force

                    // reset charge values
                    chargeTime = 0;
                    chargePower = 0;
                }

                // transition to normal state (return to rework)
                if (!isGrounded){
                    currentState = abilityState.normal;
                    anchorSwing.SetActive(false);
                    moveSpeed = baseMoveSpeed;

                    // reset charge values
                    chargeTime = 0;
                    chargePower = 0;
                }
                break;

            case abilityState.launched:
                // transition to normal state
                if (isGrounded){
                    currentState = abilityState.normal;
                    anchorSwing.SetActive(false);
                    moveSpeed = baseMoveSpeed;
                }
                break;

            default:
                break;
        }
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        manageAbilityState();

        // controls the buildup of anchor 'charge'
        if (currentState == abilityState.charging){ // must be grounded to build 'charge'
            chargeTime ++; // time spent holding button
            chargePower = chargePower + 1 / math.log(chargeTime + 1); // charge power grows with diminishing returns
        }

        


        /*if (isGrounded){ // must be grounded to build 'charge'

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
        }*/

    }
}
