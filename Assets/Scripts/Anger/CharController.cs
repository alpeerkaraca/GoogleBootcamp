using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    Rigidbody2D HeroRB; 
    Animator HeroAnimator; 

    public Transform grondCheck;  
    public LayerMask whatIsGround; 

    public float MovementSpeed; 
    public float jumpForce; 
    public float checkRadius; 
    

    private int doubleJump; 
    public int doubleJumpValue;

    private bool facingRight = true; 
    private bool isGrounded; 
    


    private void Start()
    {
        HeroRB = GetComponent<Rigidbody2D>(); 
        HeroAnimator = GetComponent<Animator>(); 
        doubleJump = doubleJumpValue; 
    }


    private void FixedUpdate()
    {
        
        isGrounded = Physics2D.OverlapCircle(grondCheck.position, checkRadius, whatIsGround);
        HorizontalMove();
    }


    private void Update()
    {
       

        if (isGrounded)
        {
            doubleJump = doubleJumpValue;
            HeroAnimator.SetBool("IsGrounded", true); 
        }
        if (!isGrounded || Input.GetKeyDown(KeyCode.Space)) 
            HeroAnimator.SetBool("IsGrounded", false); 
        if (Input.GetKeyDown(KeyCode.Space) && doubleJump > 0) 
        {
            HeroRB.velocity = Vector2.up * jumpForce; 

            doubleJump--; 

        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump == 0 && isGrounded) 
        {
            HeroRB.velocity = Vector2.up * jumpForce; 

        }



        if (HeroRB.velocity.x < 0 && facingRight) 
        {
            FlipFace();
        }
        else if (HeroRB.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }

        
    }

    void HorizontalMove() 
    {
        HeroRB.velocity = new Vector2(Input.GetAxis("Horizontal") * MovementSpeed, HeroRB.velocity.y); 
        HeroAnimator.SetFloat("playerSpeed", Mathf.Abs(HeroRB.velocity.x)); 

    }

    void FlipFace() 
    {
        facingRight = !facingRight; 
        Vector3 temporaryLocalScale = transform.localScale; 
        temporaryLocalScale.x *= -1; 
        transform.localScale = temporaryLocalScale;
    }

   
}

