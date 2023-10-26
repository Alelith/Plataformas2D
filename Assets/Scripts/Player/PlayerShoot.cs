using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    #region Attributes
    //Munition object
    [SerializeField]
    private GameObject bullet;

    //Object to control shoot
    [SerializeField]
    private Transform shootController;

    //Types of shoot
    private float chargeTime = 0;
    private bool isShooting = false;

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
        ChangeAnimation();
    }
    #endregion

    #region Input Functions
    private void GetInput()
    {
        if (Input.GetMouseButton(0))
            chargeTime += Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(Shoot());
            chargeTime = 0;
            isShooting = false;
        }
    }
    #endregion

    #region Shoot Functions
    private IEnumerator Shoot()
    {
        isShooting = true;
        yield return new WaitForSeconds(0.3f);
        if (chargeTime > 1)
            Instantiate(bullet, shootController.position, shootController.rotation).transform.localScale = new Vector3(2, 2, 2);
        else
            Instantiate(bullet, shootController.position, shootController.rotation);
    }
    #endregion

    #region Animation Functions
    private void ChangeAnimation()
    {
        animator.SetBool("isShooting", isShooting);
        animator.SetFloat("charging", chargeTime);
    }
    #endregion
}
