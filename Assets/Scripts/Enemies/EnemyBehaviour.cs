using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    #region Attributes
    //Movement
    [Header("Movement")]
    [Range(1, 10)]
    [SerializeField]
    protected float moveSpeed = 1;
    [SerializeField]
    protected int steps;
    [SerializeField]
    protected bool canIdle;
    [SerializeField]
    protected Vector2 initialDirection = Vector2.right;

    //Animation
    protected Animator animator;

    //Components
    protected Transform groundDetection;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected bool canMove = true;
    protected bool endStop = true;

    //Ground check
    protected LayerMask groundLayer;
    #endregion

    #region Unity Functions
    
    #endregion

    #region Detection Functions 
    /// <summary>
    /// Detects if the enemy is touching the ground
    /// </summary>
    /// <returns>Enemy is touching ground</returns>
    protected virtual bool GroundDetection()
    {
        return Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, groundLayer);
    }
    /// <summary>
    /// Detects if the enemy is touching the ceil
    /// </summary>
    /// <returns>Enemy is touching ceil</returns>
    protected virtual bool CeilingDetection()
    {
        return Physics2D.Raycast(groundDetection.position, Vector2.up, 1f, groundLayer);
    }
    /// <summary>
    /// Detects if the enemy is touching the wall
    /// </summary>
    /// <returns>Enemy is touching wall</returns>
    protected virtual bool WallDetection()
    {
        return (Physics2D.Raycast(groundDetection.position, Vector2.right, 1.5f, groundLayer) || Physics2D.Raycast(groundDetection.position, Vector2.left, 1.5f, groundLayer));
    }
    #endregion

    #region Movement Functions
    /// <summary>
    /// Plays and control the enemy movement
    /// </summary>
    protected abstract void EnemyMove();
    #endregion

    #region Counters
    /// <summary>
    /// Control the enemy movement and idle
    /// </summary>
    protected IEnumerator Counter()
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

    #region Animations
    /// <summary>
    /// Changes the enemy animation
    /// </summary>
    protected virtual void ChangeAnimation()
    {
        
    }
    #endregion
}

public enum MovementDirection
{
    UpDown,
    LeftRight,
    Oblicuo
}
