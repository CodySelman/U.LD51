using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using UnityEngine;

public class EvPlayerHealthChanged : IGameEvent
{
    public int HealthMax { get; }
    public int Health { get; }

    public EvPlayerHealthChanged(int health, int healthMax) {
        Health = health;
        HealthMax = healthMax;
    }
}