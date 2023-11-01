using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Attributes
    //Health
    [Header("Player Health")]
    [SerializeField]
    private int maxHealth = 10;
    private int currHealth = 10;

    //Score
    private int score = 0;

    //Other components
    private PlayerShoot playerShoot;
    private PlayerMovement playerMovement;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPowerUp"))
        {
            playerShoot.Bullets.Add(collision.GetComponent<PowerUpController>().BPU);

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("PowerUp"))
        {
            //TODO Add movement powerUps
        }
        else if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(Hurt());
            currHealth--;
        }
    }
    #endregion

    #region Other Functions
    /// <summary>
    /// Applies movement when the player gets hurt
    /// </summary>
    /// <returns></returns>
    private IEnumerator Hurt()
    {
        playerMovement.IsHurt = true;
        playerMovement.Hurt();
        yield return new WaitForSeconds(1f);
        playerMovement.IsHurt = false;
        yield return new WaitForSeconds(0.5f);
    }
    #endregion

    #region Getters & Setters
    public PlayerShoot PlayerShoot { get => playerShoot; }
    public PlayerMovement PlayerMovement { get => playerMovement; }
    public int CurrHealth { get => currHealth; }
    public int Score { get => score; }
    #endregion
}
