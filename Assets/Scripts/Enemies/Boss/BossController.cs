using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyBehaviour, IAttackerInterface
{
    #region Attributes
    [SerializeField]
    private GameObject shootPoint;
    [SerializeField]
    private GameObject bossShoot;

    private PlayerController objetive;

    [SerializeField]
    private int recoilTime;
    private float counter;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shootPoint.SetActive(false);
        animator = shootPoint.GetComponent<Animator>();
        objetive = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= recoilTime)
        {
            StartCoroutine(Attack());
            counter = 0;
        }
    }
    #endregion

    #region Other Functions
    protected override void ChangeAnimation()
    {
        
    }

    protected override void EnemyMove()
    {
        
    }

    protected override void Dead()
    {
        Instantiate(deadPrefab, transform.position, Quaternion.identity);
        GameManager.gameManager.WinGame();
        Destroy(gameObject);
    }

    public IEnumerator Attack()
    {
        //Get direction vector pointing at target
        Vector2 directionToTarget = objetive.transform.position - transform.position;

        //This assumes that your bullet sprite points to the right
        //Get the angle above the horizontal where the target is
        float angle = Vector3.Angle(Vector3.right, directionToTarget);
        //This will always be positive, so lets flip the sign if it should be negative
        if (objetive.transform.position.y < transform.position.y) angle *= -1;

        //Create a rotation that will point towards the target
        Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        shootPoint.SetActive(true);
        animator.Play("LaserAnimation");
        yield return new WaitForSeconds(0.25f);
        shootPoint.SetActive(false);
        Instantiate(bossShoot, shootPoint.transform.position, bulletRotation).GetComponent<Bullet>().IsFromPlayer = false;
        yield return null;
    }
    #endregion
}
