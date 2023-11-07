using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp/PowerUp")]
public class PowerUp : ScriptableObject
{
    #region Attributes
    [SerializeField]
    private PowerUpTypes powerUpType;
    [TextArea]
    [SerializeField]
    private string description;
    [SerializeField]
    private int score;
    [SerializeField]
    private Sprite sprite;
    #endregion

    #region Getters & Setters
    public PowerUpTypes PowerUpType { get => powerUpType; }
    public string Description { get => description; }
    public int Score { get => score; }
    public Sprite Sprite { get => sprite; }
    #endregion
}

public enum PowerUpTypes
{
    Speed,
    Jump,
    Life,
    Freeze
}
