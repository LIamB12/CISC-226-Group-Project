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
    public float moveSpeed;
    public float maxMoveSpeed;
    public float jumpForce;

    [Header("Logic")]
    [SerializeField] private bool isGrounded;
    [Tooltip("Highest possible health")]
    [SerializeField] protected float MaxHealth;
    [Tooltip("Current health")]
    [SerializeField] protected float Health;
    [Tooltip("Time in seconds for ability to recharge")]
    [SerializeField] protected float abilityCooldownMaxTime;
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
    [HideInInspector] public bool immobilized = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;


    private int moveInput;
    protected int facingDirection;
    
    public enum PlayerID
    {
        Player_1,
        Player_2
    }
    
    private void Start()
    {
        //In the inspector, choose Player_1 for WASDQ and Player_2 for IJKLU
        
        if (playerID == PlayerID.Player_1)
        {
            key_Left = KeyCode.A;
            key_Right = KeyCode.D;
            key_Up = KeyCode.W;
            key_Down = KeyCode.S;
            key_Ability = KeyCode.Q;
            facingDirection = 1;  
        }
        if (playerID == PlayerID.Player_2)
        {
            key_Left = KeyCode.J;
            key_Right = KeyCode.L;
            key_Up = KeyCode.I;
            key_Down = KeyCode.K;
            key_Ability = KeyCode.U;
            facingDirection = -1;
        }

        Health = MaxHealth;
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, 0.3f), 0, groundLayer);

        moveInput = 0;

        if (Input.GetKey(key_Left)) moveInput = -1;
        if (Input.GetKey(key_Right)) moveInput = 1;

        if(moveInput != 0)
            facingDirection = moveInput;

        if (Mathf.Abs(rb.linearVelocityX) > maxMoveSpeed)
            moveInput = 0;

        if (immobilized)
            moveInput = 0;
        
        rb.AddForce(new Vector2(moveInput * moveSpeed, 0));

        if (moveInput == 0f)
        {
            if (Mathf.Abs(rb.linearVelocityX) > 1)
            {
                rb.AddForce(new Vector2(-rb.linearVelocityX * 10, 0));
            }

        }

        if (isGrounded && Input.GetKey(key_Up))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }



        if (Input.GetKey(key_Ability) && abilityCooldownTime <= 0)
            UseAbility();

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
