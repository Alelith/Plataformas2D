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
    private Transform shootController;

    //Types of shoot
    private float chargeTime = 0;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        shootController = transform.GetChild(3);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime;
        }
        else
        {
            chargeTime = 0;
        }
    }
    #endregion

    #region Input Functions
    private void Inputs()
    {
        
    }
    #endregion
}
