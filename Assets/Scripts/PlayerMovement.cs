using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Atributes
    //Move
    [Header("Movement")]
    [Range(1, 10)]
    [SerializeField]
    private float moveSpeed = 1;
    
    [Range(2, 10)]
    [SerializeField]
    private float moveSpeedModifier = 2;

    //Jump
    [Range(1, 10)]
    [SerializeField]
    private float jumpForce = 1;

    //Others
    [SerializeField]
    private Transform groundCheckCollider;
    [SerializeField]
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
    private bool isShifting = false;
    private bool isSpacing = false;
    #endregion

    #region Unity functions
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GetInput();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        ChangeAnimation();
    }
    #endregion

    #region Movement functions
    /// <summary>
    /// Checks if the player is colliding with another collider of the ground layer
    /// </summary>
    private void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.2f, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;
        else 
            isGrounded = false;
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
            isShifting = true;
        //Gets if the LShift is not clicked
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            isShifting = false;

        //Gets if the space is clicked
        if (Input.GetButtonDown("Jump"))
            isSpacing = true;
        //Gets if the space is not clicked
        if (Input.GetButtonUp("Jump"))
            isSpacing = false;
    }

    private void Jump()
    {
        if (isGrounded && isSpacing)
        {
            //Reset the conditional booleans
            isGrounded = false;
            isSpacing = false;
            //Add jump force
            rb.AddForce(Vector2.up * jumpForce * 100);
        }
    }

    /// <summary>
    /// Calculates and starts the player movement and flip
    /// </summary>
    private void Move()
    {
        //Direction where the player is going to move in the x axis
        float xVal = horizontalAxis * moveSpeed * 100 * Time.fixedDeltaTime;

        if (isShifting)
            xVal *= moveSpeedModifier;

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
        //Set the velocity of the blend tree in the animator
        animator.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));
    }
    #endregion
}
