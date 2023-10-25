using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemiesBehaviourController : EnemyBehaviour
{
    #region Attributes
    //Movement
    private MovementDirection direction;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (initialDirection == Vector2.left || initialDirection == Vector2.right)
            direction = MovementDirection.LeftRight;
        else if (initialDirection == Vector2.up || initialDirection == Vector2.down)
            direction = MovementDirection.UpDown;

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
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
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
    #endregion

    #region Animation Functions
    protected override void ChangeAnimation()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}