using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private float parallaxSpeed;

    private Transform camera;
    private Vector3 lastCameraPosition;
    #endregion

    #region Unity Functions
    private void Start()
    {
        camera = Camera.main.transform;
        lastCameraPosition = camera.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 backgroundMovement = camera.position - lastCameraPosition;
        transform.position = new Vector3(backgroundMovement.x * parallaxSpeed, backgroundMovement.y * parallaxSpeed, 0);
    }
    #endregion
}
