using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    
    private Rigidbody2D rb;

    private bool facingRight = true;  

    private bool isGrounded;  
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Animator _animator;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    { 
        moveInput = Input.GetAxis("Horizontal");

        var velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (rb.velocity != velocity) rb.velocity = velocity;
        

        if ((!facingRight && moveInput > 0) || (facingRight && moveInput < 0))
        {
            Flip();
        }

        _animator.SetBool("isRun", moveInput!=0);
    }

    private void Update() 
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpForce;
            _animator.SetTrigger("takeOf");
        }
        
        _animator.SetBool("isJump", !isGrounded);
    }

    void Flip()
    {
        facingRight = !facingRight;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
