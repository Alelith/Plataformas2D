using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletPowerUp", menuName = "PowerUp/Bullet")]
public class BulletPowerUp : ScriptableObject
{
    [SerializeField]
    private BulletPowerUpTypes bulletPowerUpType;
    [SerializeField]
    private string description;
    [SerializeField]
    private GameObject bulletPrefab;

    public BulletPowerUpTypes BulletPowerUpTypes { get => bulletPowerUpType; }
    public string Description { get => description; }
    public GameObject BulletPrefab { get => bulletPrefab; }
}

public enum BulletPowerUpTypes
{
    
}
