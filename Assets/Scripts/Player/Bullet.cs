using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject destroyPrefab;

    private bool isFromPlayer;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isFromPlayer)
        {
            Instantiate(destroyPrefab, transform.position, Quaternion.identity);

            collision.GetComponent<EnemyBehaviour>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player") && !isFromPlayer)
        {
            Instantiate(destroyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public bool IsFromPlayer { get => isFromPlayer; set => isFromPlayer = value; }
}
