using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private float lifetime;
    #endregion

    #region Unity Functions 
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    #endregion
}
