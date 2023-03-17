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
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight==false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight==true && moveInput < 0)
        {
            Flip();
        }

        if (moveInput==0)
        {
            _animator.SetBool("isRun", false);
        }
        else
        {
            _animator.SetBool("isRun", true);
        }
    }

    private void Update() 
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpForce;
            _animator.SetTrigger("takeOf");
        }

        if (isGrounded==true)
        {
            _animator.SetBool("isJump", false);
        }
        else
        {
            _animator.SetBool("isJump", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        // if (moveInput<0)
        // {
        //     transform.eulerAngles = new Vector3(0, 180, 0);
        // }
        // else if (moveInput>0)
        // {
        //     transform.eulerAngles = new Vector3(0, 0, 0);
        // }
    }
}
