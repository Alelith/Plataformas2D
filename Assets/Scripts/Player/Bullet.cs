using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject destroyPrefab;

    private bool isFromPlayer;
    #endregion

    #region Unity Functions
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isFromPlayer)
        {
            AudioManager.instance.PlayDamageSound();
            Instantiate(destroyPrefab, transform.position, Quaternion.identity);

            var enemy = collision.GetComponent<EnemyBehaviour>();
            enemy.TakeDamage(damage);

            GameObject.Find("Player").GetComponent<PlayerController>().Score += enemy.Score;

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player") && !isFromPlayer)
        {
            Instantiate(destroyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    #endregion

    #region Getters & Setters
    public bool IsFromPlayer { get => isFromPlayer; set => isFromPlayer = value; }
    #endregion
}
