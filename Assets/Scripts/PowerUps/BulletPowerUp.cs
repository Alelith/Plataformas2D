using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletPowerUp", menuName = "PowerUp/Bullet")]
public class BulletPowerUp : ScriptableObject
{
    [SerializeField]
    private BulletPowerUpTypes bulletPowerUpType;
    [TextArea]
    [SerializeField]
    private string description;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int score;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int energyPay;

    public BulletPowerUpTypes BulletPowerUpType { get => bulletPowerUpType; }
    public string Description { get => description; }
    public GameObject BulletPrefab { get => bulletPrefab; }
    public int Score { get => score; }
    public Sprite Sprite { get => sprite; }
    public int EnergyPay { get => energyPay; }
}

public enum BulletPowerUpTypes
{
    Default,
    Bolt,
    Charged,
    Crossed,
    Pulse,
    Spark,
    Waveform
}
