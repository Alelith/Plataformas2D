using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Attributes
    [Header("Player Health")]
    [SerializeField]
    private float health = 10;

    [Header("Blink")]
    [SerializeField]
    private float blinkSpeed = 2.5f;

    [HideInInspector]
    public int powerUp = 0;

    private bool canTakeDamage = true;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            //AudioManager.instance.PlayPowerUpSound();

            Destroy(collision.gameObject);

            powerUp++;

            /*if (powerUp >= GameManager.gameManager.powerUps)
            {
                GameManager.gameManager.WinGame();
            }*/
        }
    }
    #endregion

    #region Damage Functions
    /// <summary>
    /// Manages Player Damage
    /// </summary>
    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            //AudioManager.instance.PlayDamageSound();

            health--;

            if (health <= 0)
            {
                //GameManager.gameManager.LoseGame();
            }
        }
    }
    #endregion

    #region Getters & Setters
    //Getters & Setters of private classes
    public float Health { get => health; set => health = value; }
    public int PowerUp { get => powerUp; set => powerUp = value; }
    #endregion
}
