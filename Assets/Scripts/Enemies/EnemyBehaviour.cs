using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    #region Attributes
    //Components
    protected Transform groundDetection;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;

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
    protected bool GroundDetection()
    {
        return Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, groundLayer);
    }
    /// <summary>
    /// Detects if the enemy is touching the ceil
    /// </summary>
    /// <returns>Enemy is touching ceil</returns>
    protected bool CeilingDetection()
    {
        return Physics2D.Raycast(groundDetection.position, Vector2.up, 1f, groundLayer);
    }
    /// <summary>
    /// Detects if the enemy is touching the wall
    /// </summary>
    /// <returns>Enemy is touching wall</returns>
    protected bool WallDetection()
    {
        var direction = (spriteRenderer.flipX) ? Vector2.right : Vector2.left;
        return Physics2D.Raycast(groundDetection.position, direction, 1.5f, groundLayer);
    }
    #endregion

    #region Movement Functions
    /// <summary>
    /// Plays and control the enemy movement
    /// </summary>
    protected virtual void EnemyMove()
    {

    }
    #endregion
}
