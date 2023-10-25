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
    #endregion

    #region Unity Functions
    private void Update()
    {
        GetInput();
    }
    #endregion

    #region Input Functions
    private void GetInput()
    {
        if (Input.GetMouseButton(0))
            chargeTime += Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
            Shoot();
    }
    #endregion

    #region Shoot Functions
    private void Shoot()
    {
        if (chargeTime > 1)
            Instantiate(bullet, shootController.position, shootController.rotation).transform.localScale = new Vector3(2, 2, 2);
        else
            Instantiate(bullet, shootController.position, shootController.rotation);
    }
    #endregion
}
