using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp/PowerUp")]
public class PowerUp : ScriptableObject
{
    [SerializeField]
    private PowerUpTypes powerUpType;
    [SerializeField]
    private string description;
    [SerializeField]
    private int score;
    [SerializeField]
    private Sprite sprite;

    public PowerUpTypes PowerUpType { get => powerUpType; }
    public string Description { get => description; }
    public int Score { get => score; }
    public Sprite Sprite { get => sprite; }
}

public enum PowerUpTypes
{
    Speed,
    Jump,
    Life,
    Freeze
}
