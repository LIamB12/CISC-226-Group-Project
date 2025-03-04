using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 50;
    public float maxMoveSpeed = 5;
    public float jumpForce = 400;

    [Header("Logic")]
    [SerializeField] private bool isGrounded;
    [Tooltip("Highest possible health")]
    [SerializeField] protected float MaxHealth = 100;
    [Tooltip("Current health")]
    [SerializeField] protected float Health;
    [Tooltip("Time in seconds for ability to recharge")]
    [SerializeField] protected float abilityCooldownMaxTime = 1;
    [Tooltip("Current time in seconds until ability recharges")]
    [SerializeField] protected float abilityCooldownTime;

    [Header("Assigned Fields")]
    [Tooltip("Player_1 for WASDQ, Player_2 for IJKLU")]
    [SerializeField] public PlayerID playerID;
    [SerializeField] public Sprite ClassIcon;
    [HideInInspector] public KeyCode key_Left;
    [HideInInspector] public KeyCode key_Right;
    [HideInInspector] public KeyCode key_Up;
    [HideInInspector] public KeyCode key_Down;
    [HideInInspector] public KeyCode key_Ability;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public Rigidbody2D rb;


    private int moveInput;
    private int BelowMaxSpeed;
    public int facingDirection;
    
    public enum PlayerID
    {
        Player_1,
        Player_2
    }
    
    private void Start()
    {
        //In the inspector, choose Player_1 for WASD and Player_2 for PL;'
        
        if (playerID == PlayerID.Player_1)
        {
            key_Left = KeyCode.A;
            key_Right = KeyCode.D;
            key_Up = KeyCode.W;
            key_Down = KeyCode.S;
            key_Ability = KeyCode.LeftShift;
            facingDirection = 1;  
        }
        if (playerID == PlayerID.Player_2)
        {
            key_Left = KeyCode.L;
            key_Right = KeyCode.Quote;
            key_Up = KeyCode.P;
            key_Down = KeyCode.Semicolon;
            key_Ability = KeyCode.RightShift;
            facingDirection = -1;
        }

        Health = MaxHealth;
        
    }


    public void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, 0.1f), 0, groundLayer);

        moveInput = 0;

        if (Input.GetKey(key_Left)) moveInput = -1;
        if (Input.GetKey(key_Right)) moveInput = 1;

        if (moveInput != 0)
           facingDirection = moveInput;

        
        
        //Old, Unworking, Bullshit-ass experimental physics movement testing.
        /*BelowMaxSpeed = 1;

        if ((rb.linearVelocityX > maxMoveSpeed) && moveInput == 1)
            BelowMaxSpeed = 0;        
        if ((rb.linearVelocityX < -maxMoveSpeed) && moveInput == -1)
            BelowMaxSpeed = 0;

        if ((moveInput == 0f) && isGrounded)
        {
            //if(BelowMaxSpeed == 1) 
                //rb.AddForce(new Vector2(-rb.linearVelocityX * (rb.mass * 5), 0));
        } */
        
        

        if (Mathf.Abs(rb.linearVelocityX) > maxMoveSpeed)
        {
            rb.linearVelocityX = Mathf.Sign(rb.linearVelocityX) * maxMoveSpeed;
        }

        if (!GameInstance.PlayersImmobilized)
        {
            rb.AddForce(new Vector2(moveInput * moveSpeed, 0));

            if (isGrounded && rb.linearVelocityY <= 0 && Input.GetKey(key_Up))
                rb.AddForce(new Vector2(0, jumpForce));
            
            if (Input.GetKey(key_Ability) && abilityCooldownTime <= 0)
                UseAbility();
        }

        if(abilityCooldownTime > 0)
        {
            abilityCooldownTime -= Time.fixedDeltaTime;
            if (abilityCooldownTime < 0)
                abilityCooldownTime = 0;
        }

    }

    protected virtual void UseAbility()
    {
        //Your ability logic goes here in the subclass
    }
    
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    } 
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
