using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Attributes
    //Move
    [Header("Movement")]
    [Range(1, 10)]
    [SerializeField]
    private float moveSpeed = 1;
    [Range(2, 10)]
    [SerializeField]
    private float runSpeedModifier = 2;
    [Range(1, 10)]
    [SerializeField]
    private float crouchSpeedModifier = 2;
    [Range(1, 4)]
    [SerializeField]
    private int totalJumps = 2;
    private int availableJumps;

    //Jump
    [Header("Jump")]
    [Range(1, 10)]
    [SerializeField]
    private float jumpForce = 1;

    //Ground & Overhead
    [Header("Ground & Overhead")]
    [SerializeField]
    private Transform groundCheckCollider;
    [SerializeField]
    private Transform overheadCheckCollider;
    [SerializeField]
    Collider2D standCollider;
    [SerializeField]
    Collider2D crouchCollider;
    private LayerMask groundLayer;

    //Components
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    //Player Info
    private bool facingRight = true;
    private bool isGrounded = false;

    //Inputs
    private float horizontalAxis = 0;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isDoubleJumping = false;
    private bool isCrouching = false;
    #endregion

    #region Unity functions
    private void Awake()
    {
        availableJumps = totalJumps;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        GetInput();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Move();
        Crouch();
        ChangeAnimation();
    }
    #endregion

    #region Checks & Inputs Functions
    /// <summary>
    /// Checks if the player is colliding with another collider of the ground layer
    /// </summary>
    private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.2f, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            availableJumps = totalJumps;
            animator.SetBool("isDoubleJumping", false);
        }
        else
            isGrounded = false;

        animator.SetBool("isJumping", !isGrounded);
    }

    /// <summary>
    /// Gets the keyboard and mouse input
    /// </summary>
    private void GetInput()
    {
        //Gets the horizontal value
        horizontalAxis = Input.GetAxisRaw("Horizontal");

        //Gets if the LShift is clicked
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        //Gets if the LShift is not clicked
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        //If we press Jump button enable jump 
        if (Input.GetButtonDown("Jump"))
            Jump();

        //If we press Crouch button enable crouch 
        if (Input.GetButtonDown("Crouch"))
            isCrouching = true;
        //Otherwise disable it
        else if (Input.GetButtonUp("Crouch"))
            isCrouching = false;
    }
    #endregion

    #region Movement Functions
    /// <summary>
    /// Applies force to the player to jump
    /// </summary>
    private void Jump()
    {
        if (isGrounded)
        {
            isDoubleJumping = true;
            availableJumps--;

            //Add jump force
            rb.velocity = Vector2.up * jumpForce;
        }
        else
        {
            if (isDoubleJumping && availableJumps > 0)
            {
                availableJumps--;

                //Add jump force
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("isDoubleJumping", true);
            }
        }
    }

    /// <summary>
    /// Applies the crouch collider and animations
    /// </summary>
    private void Crouch()
    {
        if (!isCrouching)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position, 0.2f, groundLayer))
                isCrouching = true;
        }

        crouchCollider.enabled = isCrouching;
        standCollider.enabled = !isCrouching;
        animator.SetBool("isCrouching", isCrouching);
    }

    /// <summary>
    /// Calculates and starts the player movement and flip
    /// </summary>
    private void Move()
    {
        //Direction where the player is going to move in the x axis
        float xVal = horizontalAxis * moveSpeed * 100 * Time.fixedDeltaTime;

        if (isRunning)
            xVal *= runSpeedModifier;
        else if (isCrouching)
            xVal /= crouchSpeedModifier;

        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Movement of the player using the rigid body
        rb.velocity = targetVelocity;

        //Checks if the player is looking right and move to the left
        if (facingRight && horizontalAxis < 0)
        {
            sprite.flipX = true;
            facingRight = false;
        }
        //Checks if the player is looking left and move to the right
        else if (!facingRight && horizontalAxis > 0)
        {
            sprite.flipX = false;
            facingRight = true;
        }
    }

    /// <summary>
    /// Changes the animations of the player
    /// </summary>
    private void ChangeAnimation()
    {
        //Set the xVelocity of the blend tree in the animator
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        //Set the yVelocity of the blend tree in the animator
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    #endregion
}
