using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp
{
    public PowerUpTypes PowerUpType { get; set; }
    public string Description { get; set; }
    public Action Effect { get; set; }
}

public enum PowerUpTypes
{
    Speed,
    Jump,
    Life,
    Damage,
    Freeze
}
