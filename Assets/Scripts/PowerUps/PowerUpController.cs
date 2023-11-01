using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField]
    private BulletPowerUp bpu;
    [SerializeField]
    private PowerUp pu;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (bpu != null)
            sprite.sprite = bpu.Sprite;
        else
            sprite.sprite = pu.Sprite;
    }

    #region Getters & Setters
    public BulletPowerUp BPU { get => bpu; }
    public PowerUp PU { get => pu; }
    #endregion
}
