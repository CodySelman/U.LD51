using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using UnityEngine;

public class EvShipHealthChanged : IGameEvent
{
    public int HealthMax { get; }
    public int Health { get; }

    public EvShipHealthChanged(int health, int healthMax) {
        Health = health;
        HealthMax = healthMax;
    }
}