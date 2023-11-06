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
    private Animator animator;

    //Player Info
    private bool facingRight = true;
    private bool isGrounded = false;

    //Inputs
    private float horizontalAxis = 0;
    private bool isRunning = false;
    private bool isDoubleJumping = false;
    private bool isCrouching = false;
    private bool isHurt = false;
    private bool canStand = true;
    #endregion

    #region Unity functions
    private void Awake()
    {
        availableJumps = totalJumps;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        GetInput();
        GroundCheck();
        CanStand();
    }

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
            Crouch();
        }
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
    /// Checks if the player is colliding with another collider of the ground layer
    /// </summary>
    private void CanStand()
    {
        canStand = !Physics2D.Raycast(overheadCheckCollider.transform.position, Vector2.up, 0.5f, groundLayer);
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
        if (Input.GetButton("Crouch"))
            isCrouching = true;
        //If we press Crouch button enable crouch 
        else if (!Input.GetButton("Crouch") && canStand)
            isCrouching = false;
    }
    #endregion

    #region Movement Functions
    /// <summary>
    /// Applies force to the player to jump
    /// </summary>
    private void Jump()
    {
        if (isGrounded && !isCrouching)
        {
            isDoubleJumping = true;
            availableJumps--;

            //Add jump force
            rb.velocity = Vector2.up * jumpForce;
        }
        else if (!isGrounded)
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
        if (isGrounded)
        {
            if (!canStand)
            {
                if (rb.velocity.x == 0)
                {
                    isCrouching = true;
                    animator.speed = 0;
                }
                else if (rb.velocity.x != 0)
                {
                    isCrouching = true;
                    animator.speed = 1;
                }
            }
        }
        else
            isCrouching = false;

        crouchCollider.enabled = isCrouching;
        standCollider.enabled = !isCrouching;
    }

    /// <summary>
    /// Calculates and starts the player movement and flip
    /// </summary>
    private void Move()
    {
        //Direction where the player is going to move in the x axis
        float xVal = horizontalAxis * moveSpeed * 100 * Time.fixedDeltaTime;

        if (isRunning && !isCrouching)
            xVal *= runSpeedModifier;
        else if (isCrouching)
            xVal /= crouchSpeedModifier;

        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Movement of the player using the rigid body
        rb.velocity = targetVelocity;

        //Checks if the player is looking right and move to the left
        if (facingRight && horizontalAxis < 0)
        {
            transform.eulerAngles = new Vector3 (0, 180, 0);
            facingRight = false;
        }
        //Checks if the player is looking left and move to the right
        else if (!facingRight && horizontalAxis > 0)
        {
            transform.eulerAngles = Vector3.zero;
            facingRight = true;
        }
    }
    #endregion

    #region Animation Functions
    /// <summary>
    /// Changes the animations of the player
    /// </summary>
    private void ChangeAnimation()
    {
        //Set the xVelocity of the blend tree in the animator
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        //Set the yVelocity of the blend tree in the animator
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isHurt", isHurt);
        animator.SetBool("canStand", canStand);
        animator.SetBool("isCrouching", isCrouching);
    }
    #endregion

    #region Other Functions
    /// <summary>
    /// Applies movement when the player gets hurt
    /// </summary>
    /// <returns></returns>
    public void Hurt() => rb.velocity = new Vector2(-rb.velocity.x * moveSpeed * 20 * Time.fixedDeltaTime, Mathf.Abs(rb.velocity.y) * moveSpeed * 10 * Time.fixedDeltaTime);
    #endregion

    #region Getters & Setters
    public bool IsHurt { get => isHurt; set => isHurt = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float RunSpeedModifier { get => runSpeedModifier; set => runSpeedModifier = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    #endregion
}
