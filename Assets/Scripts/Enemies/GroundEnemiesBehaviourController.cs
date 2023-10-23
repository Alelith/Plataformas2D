using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemiesBehaviourController : EnemyBehaviour
{
    #region Attributes
    //Detection
    protected Transform wallDetection;
    #endregion

    #region Unity Functions
    protected virtual void Awake()
    {
        groundDetection = transform.GetChild(0);
        wallDetection = transform.GetChild(1);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        if (canIdle)
            StartCoroutine(Counter());
    }

    protected virtual void FixedUpdate()
    {
        if (endStop && canIdle)
            StartCoroutine(Counter());
        if (canIdle)
            ChangeAnimation();
        EnemyMove();
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
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
            }
            targetVelocity = new Vector2(initialDirection.x * moveSpeed * 100 * Time.fixedDeltaTime, rb.velocity.y);
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
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
    #endregion
}
