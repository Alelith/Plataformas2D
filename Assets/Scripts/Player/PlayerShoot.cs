using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    #region Attributes
    //Munition object
    [SerializeField]
    private List<BulletPowerUp> bullets;

    //Object to control shoot
    [SerializeField]
    private Transform shootController;

    //Types of shoot
    private float chargeTime = 0;
    private int currentBullet;

    //Animator
    private Animator animator;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GetInput();
        ChangeChargeAnimation();
    }
    #endregion

    #region Input Functions
    private void GetInput()
    {
        if (Input.GetMouseButton(0) && animator.GetFloat("xVelocity") == 0)
            chargeTime += Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {
            if ((animator.GetBool("isCrouching") && animator.GetFloat("xVelocity") == 0 && animator.GetBool("canStand")) || (!animator.GetBool("isCrouching")))
            {
                StartCoroutine(ChangeShootAnimation());
                Shoot();
                chargeTime = 0;
            }
        }
        if (Input.GetMouseButtonDown(1))
            currentBullet = (currentBullet + 1) % bullets.Count;
    }
    #endregion

    #region Shoot Functions
    private void Shoot()
    {
        if (chargeTime > 1)
            Instantiate(bullets[currentBullet].BulletPrefab, shootController.position, shootController.rotation).transform.localScale = new Vector3(2, 2, 2);
        else if (chargeTime < 1)
            Instantiate(bullets[currentBullet].BulletPrefab, shootController.position, shootController.rotation);
    }
    #endregion

    #region Animation Functions
    private IEnumerator ChangeShootAnimation()
    {
        animator.SetBool("isShooting", true);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("isShooting", false);
    }
    private void ChangeChargeAnimation()
    {
        animator.SetFloat("charging", chargeTime);
    }
    #endregion

    #region Getters & Setters
    public List<BulletPowerUp> Bullets { get => bullets; set => bullets = value; }
    public int CurrentBullet { get => currentBullet; set => currentBullet = value; }
    #endregion
}
