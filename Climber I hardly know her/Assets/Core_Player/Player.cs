using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;          
    public float jumpForce;

    [Header("Logic")]
    [SerializeField] private bool isGrounded;
    [SerializeField] protected float abilityCooldown;
    [SerializeField] protected float abilityCooldownCounter;

    [Header("Assigned Fields")]
    [SerializeField] private PlayerID playerID;
    private KeyCode Left;
    private KeyCode Right;
    private KeyCode Up;
    private KeyCode Down;
    private KeyCode Ability;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] protected GameObject projectile;
    [SerializeField] private Rigidbody2D rb;


    private int moveInput;
    protected int facingDirection;
    
    public enum PlayerID
    {
        Player_1,
        Player_2
    }
    
    private void Awake()
    {
        if (playerID == PlayerID.Player_1)
        {
            Left = KeyCode.A;
            Right = KeyCode.D;
            Up = KeyCode.W;
            Down = KeyCode.S;
            Ability = KeyCode.Q;
        }
        if (playerID == PlayerID.Player_2)
        {
            Left = KeyCode.J;
            Right = KeyCode.L;
            Up = KeyCode.I;
            Down = KeyCode.K;
            Ability = KeyCode.U;
        }
    }


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        moveInput = 0;

        if (Input.GetKey(Left)) moveInput = -1;
        if (Input.GetKey(Right)) moveInput = 1;

        if(moveInput != 0)
                    facingDirection = moveInput;

        if (Mathf.Abs(rb.linearVelocityX) > 5f)
            moveInput = 0;

        rb.AddForce(new Vector2(moveInput * moveSpeed, 0));

        if (moveInput == 0f)
        {
            if (Mathf.Abs(rb.linearVelocityX) > 1)
            {
                rb.AddForce(new Vector2(-rb.linearVelocityX * 10, 0));
            }

        }

        if (isGrounded && Input.GetKey(Up))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }



        if (Input.GetKey(Ability) && abilityCooldownCounter <= 0)
            UseAbility();

        if(abilityCooldownCounter > 0)
        {
            abilityCooldownCounter -= Time.fixedDeltaTime;
            if (abilityCooldownCounter < 0)
                abilityCooldownCounter = 0;
        }

    }

    public virtual void UseAbility()
    {
        GameObject newProjectile = Instantiate(projectile, (Vector2)this.transform.position + new Vector2(1.5f * facingDirection, 0), Quaternion.identity);
        abilityCooldownCounter = abilityCooldown;
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(8.5f * facingDirection, 5);
        Debug.Log("super");
    }
}
