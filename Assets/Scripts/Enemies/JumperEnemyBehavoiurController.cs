using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemyBehavoiurController : GroundEnemiesBehaviourController, IAttackerInterface
{
    #region Attributes
    //Movement
    [SerializeField]
    private int jumpSteps;
    private bool isJumping;
    #endregion

    #region Unity Functions
    protected override void Start()
    {
        if (canIdle)
            StartCoroutine(Counter());
    }

    protected override void FixedUpdate()
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
                spriteRenderer.flipX = !spriteRenderer.flipX;
                wallDetection.localPosition = new Vector3(-wallDetection.localPosition.x, wallDetection.localPosition.y);
            }
            targetVelocity = new Vector2(initialDirection.x * moveSpeed * 100 * Time.fixedDeltaTime, rb.velocity.y);
        }
        else
        {
            targetVelocity = Vector2.zero;
        }
        rb.velocity = targetVelocity;
    }

    private IEnumerator JumpCounter()
    {
        endStop = false;
        for (int i = 0; i < jumpSteps; i++)
        {
            yield return new WaitForSeconds(1);
        }
        canMove = false;
        yield return new WaitForSeconds(3);
        canMove = true;
        endStop = true;
    }

    public IEnumerator Attack()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Animation Functions
    protected override void ChangeAnimation()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", Mathf.Abs(rb.velocity.y));
    }
    #endregion

    #region Detection Functions
    protected override bool WallDetection()
    {
        var direction = (spriteRenderer.flipX) ? Vector2.left : Vector2.right;
        return Physics2D.Raycast(wallDetection.position, direction, 1f, groundLayer);
    }
    #endregion
}
