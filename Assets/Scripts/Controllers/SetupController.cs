using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupController : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private Vector3 playerInitialPosition;
    #endregion

    #region Unity Functions
    private void Start()
    {
        var player = GameObject.Find("Player");
        virtualCamera.Follow = player.transform;
        player.transform.position = playerInitialPosition;
    }
    #endregion
}
