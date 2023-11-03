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

            score += (collision.GetComponent<PowerUpController>().BPU.Score * 10);

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("PowerUp"))
        {
            if (collision.GetComponent<PowerUpController>().PU.PowerUpType == PowerUpTypes.Jump)
                playerMovement.JumpForce *= 2;
            else if (collision.GetComponent<PowerUpController>().PU.PowerUpType == PowerUpTypes.Speed)
            {
                playerMovement.MoveSpeed *= 2;
                playerMovement.RunSpeedModifier *= 2;
            }
            else if (collision.GetComponent<PowerUpController>().PU.PowerUpType == PowerUpTypes.Life)
            {
                if (collision.GetComponent<PowerUpController>().PU.Score == 25)
                    currHealth = Mathf.Clamp(currHealth++, 1, 10);
                else if (collision.GetComponent<PowerUpController>().PU.Score == 50)
                    currHealth = Mathf.Clamp(currHealth += 2, 1, 10);
            }

            score += (collision.GetComponent<PowerUpController>().PU.Score * 10);

            Destroy(collision.gameObject);
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
