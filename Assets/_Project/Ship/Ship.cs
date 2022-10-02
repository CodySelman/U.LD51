using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship Instance;

    public Transform playerSpawnPoint;
    
    [SerializeField] int healthMax = 10;
    [SerializeField] int health = 10;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        health = healthMax;
    }

    public void GetHit() {
        
    }
}
