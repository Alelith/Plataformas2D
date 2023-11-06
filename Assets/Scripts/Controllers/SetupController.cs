using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private Vector3 playerInitialPosition;

    private void Start()
    {
        var player = GameObject.Find("Player");
        virtualCamera.Follow = player.transform;
        player.transform.position = playerInitialPosition;
    }
}
