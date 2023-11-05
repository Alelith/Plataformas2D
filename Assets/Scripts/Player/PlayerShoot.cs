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

    //Energy
    [SerializeField]
    private int maxEnergy;
    [SerializeField]
    private int currEnergy;
    [SerializeField]
    private int energyFillCount;
    [SerializeField]
    private int energyFillTime;
    private float energyCounter;

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
        CanCharge();
    }
    #endregion

    #region Input Functions
    private void GetInput()
    {
        if (Input.GetMouseButton(0) && animator.GetFloat("xVelocity") == 0)
            chargeTime += Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {
            if (((animator.GetBool("isCrouching") && animator.GetFloat("xVelocity") == 0 && animator.GetBool("canStand")) || (!animator.GetBool("isCrouching"))) && currEnergy >= bullets[currentBullet].EnergyPay)
            {
                StartCoroutine(ChangeShootAnimation());
                Shoot();
                currEnergy -= bullets[currentBullet].EnergyPay;
            }
            chargeTime = 0;
        }
        if (Input.GetMouseButtonDown(1))
            currentBullet = (currentBullet + 1) % bullets.Count;
    }
    #endregion

    #region Shoot Functions
    private void Shoot()
    {
        if (chargeTime > 1)
            Instantiate(bullets[currentBullet].BulletPrefab, shootController.position, shootController.rotation).GetComponent<Bullet>().IsFromPlayer = true;
        else if (chargeTime < 1)
            Instantiate(bullets[currentBullet].BulletPrefab, shootController.position, shootController.rotation).GetComponent<Bullet>().IsFromPlayer = true;
    }

    private void CanCharge()
    {
        energyCounter += Time.deltaTime;
        if (energyCounter >= energyFillTime)
        {
            energyCounter = 0;
            currEnergy = Mathf.Clamp(currEnergy += energyFillCount, 0, maxEnergy);
        }
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
    public int CurrEnergy { get => currEnergy; set => currEnergy = value; }
    public int MaxEnergy { get => maxEnergy; set => maxEnergy = value; }
    #endregion
}
