using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemiesBehaviourController : EnemyBehaviour
{
    #region Attributes
    //Movement
    [Header("Movement")]
    [Range(1, 10)]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int steps;
    private Vector2 initialDirection = Vector2.right;
    private bool canMove = true;
    private bool endStop = true;

    //Animation
    private Animator animator;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        groundDetection = transform.GetChild(0);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Counter());
    }

    private void FixedUpdate()
    {
        if (endStop)
            StartCoroutine(Counter());
        EnemyMove();
        ChangeAnimation();
    }
    #endregion

    #region Movement Functions
    protected override void EnemyMove()
    {
        Vector2 targetVelocity;

        if (canMove)
        {
            if (WallDetection() || !GroundDetection())
            {
                initialDirection = -initialDirection;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
            targetVelocity = new Vector2(initialDirection.x * moveSpeed * 100 * Time.fixedDeltaTime, rb.velocity.y);
        }
        else
        {
            targetVelocity = Vector2.zero;
        }
        rb.velocity = targetVelocity;
    }

    private IEnumerator Counter()
    {
        endStop = false;
        for (int i = 0; i < steps; i++)
        {
            yield return new WaitForSeconds(1);
        }
        canMove = false;
        yield return new WaitForSeconds(3);
        canMove = true;
        endStop = true;
    }
    #endregion

    #region Animation Functions
    private void ChangeAnimation()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
    #endregion
}
