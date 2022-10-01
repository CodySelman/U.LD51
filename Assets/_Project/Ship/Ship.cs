using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] int healthMax = 10;
    [SerializeField] int health = 10;

    void Start() {
        health = healthMax;
    }
}
