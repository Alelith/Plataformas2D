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
            AudioManager.instance.PlayPowerUpSound();
            playerShoot.Bullets.Add(collision.GetComponent<PowerUpController>().BPU);

            score += (collision.GetComponent<PowerUpController>().BPU.Score * 10);

            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("PowerUp"))
        {
            AudioManager.instance.PlayPowerUpSound();
            if (collision.GetComponent<PowerUpController>().PU.PowerUpType == PowerUpTypes.Jump)
                playerMovement.JumpForce *= 1.25f;
            else if (collision.GetComponent<PowerUpController>().PU.PowerUpType == PowerUpTypes.Speed)
            {
                playerMovement.MoveSpeed *= 1.25f;
                playerMovement.RunSpeedModifier *= 1.25f;
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
            AudioManager.instance.PlayDamageSound();
            StartCoroutine(Hurt());
        }
        else if (collision.CompareTag("Bullet"))
        {
            AudioManager.instance.PlayDamageSound();
            if (!collision.GetComponent<Bullet>().IsFromPlayer)
                StartCoroutine(Hurt());
        }
        else if (collision.CompareTag("DeadZone"))
        {
            currHealth = 1;
            StartCoroutine(Hurt());
        }
    }
    #endregion

    #region Other Functions
    /// <summary>
    /// Applies movement when the player gets hurt
    /// </summary>
    /// <returns></returns>
    public IEnumerator Hurt()
    {
        currHealth--;
        if (currHealth > 0)
        {
            playerMovement.IsHurt = true;
            playerMovement.Hurt();
            yield return new WaitForSeconds(1f);
            playerMovement.IsHurt = false;
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            GameManager.gameManager.LoseGame();
        }
    }
    #endregion

    #region Getters & Setters
    public PlayerShoot PlayerShoot { get => playerShoot; }
    public PlayerMovement PlayerMovement { get => playerMovement; }
    public int CurrHealth { get => currHealth; }
    public int Score { get => score; set => score = value; }
    #endregion
}
