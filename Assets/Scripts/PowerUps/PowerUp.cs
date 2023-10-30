using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp/PowerUp")]
public class PowerUp : ScriptableObject
{
    public PowerUpTypes PowerUpType { get; set; }
    public string Description { get; set; }
}

public enum PowerUpTypes
{
    Speed,
    Jump,
    Life,
    Freeze
}
