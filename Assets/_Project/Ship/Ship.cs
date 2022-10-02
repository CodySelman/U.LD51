using System;
using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship Instance;

    public Transform playerSpawnPoint;
    
    [SerializeField] int healthMax = 10;
    [SerializeField] int health = 10;
    [SerializeField] float invincibilityTime = 0.5f;
    
    bool _isInvincible = false;
    float _invincibilityTimer = 0f;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        SetHealth(healthMax);
    }

    void Update() {
        if (GameManager.Instance.isGameOver || UpgradeManager.Instance.isUpgrading) return;
        
        if (_isInvincible) {
            _invincibilityTimer -= Time.deltaTime;
            if (_invincibilityTimer <= 0f) {
                _isInvincible = false;
            }
        }
    }

    public void GetHit() {
        if (_isInvincible || GameManager.Instance.isGameOver || UpgradeManager.Instance.isUpgrading) return;
        SoundManager.Instance.BaseHurt();
        SetHealth(health - 1);
        _isInvincible = true;
        _invincibilityTimer = invincibilityTime;
    }

    public void RecoverHealth(int amount) {
        SetHealth(health += amount);
    }

    public void IncreaseHealthMax(int amount) {
        SetHealthMax(healthMax += amount);
    }

    void SetHealth(int newHealth) {
        health = Mathf.Min(newHealth, healthMax);
        EvShipHealthChanged e = new (health, healthMax);
        EventManager.instance.Raise(e);
        if (health <= 0) {
            GameManager.Instance.GameOver();
        }
    }

    void SetHealthMax(int newHealthMax) {
        healthMax = newHealthMax;
        if (health > healthMax) {
            health = healthMax;
        }
        EvShipHealthChanged e = new (health, healthMax);
        EventManager.instance.Raise(e);
    }
}
