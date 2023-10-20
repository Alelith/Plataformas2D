using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemiesBehaviourController : EnemyBehaviour
{
    #region Attributes
    //Movement
    [Header("Movement")]
    [Range(1, 10)]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int steps;
    [SerializeField]
    private MovementDirection direction;
    private Vector2 initialDirection;
    private bool canMove = true;
    private bool endStop = true;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (direction == MovementDirection.LeftRight)
        {
            initialDirection = Vector2.left;
        }
        else
        {
            initialDirection = Vector2.up;
        }
        groundDetection = transform.GetChild(0);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
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
    }
    #endregion

    #region Movement Functions
    protected override void EnemyMove()
    {
        Vector2 targetVelocity;
        
        if (this.direction == MovementDirection.LeftRight && canMove)
        {
            if (WallDetection())
            {
                initialDirection = -initialDirection;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
            targetVelocity = new Vector2(initialDirection.x * moveSpeed * 100 * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (this.direction == MovementDirection.UpDown && canMove)
        {
            if (WallDetection())
            {
                initialDirection = -initialDirection;
            }
            targetVelocity = new Vector2(rb.velocity.x, initialDirection.y * moveSpeed * 100 * Time.fixedDeltaTime);
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
}

public enum MovementDirection
{
    UpDown,
    LeftRight,
    Oblicuo
}