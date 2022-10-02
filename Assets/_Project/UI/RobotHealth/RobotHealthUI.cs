using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using CodTools.Utilities;

public class RobotHealthUI : MonoBehaviour
{
    public static RobotHealthUI Instance;

    [SerializeField] HorizontalLayoutGroup heartContainer;
    [SerializeField] Image robotHeartPrefab;
    [SerializeField] Sprite fullHeartSprite;
    [SerializeField] Sprite emptyHeartSprite;

    List<Image> heartPrefabs = new();
    int _healthMax = 3;
    int _health = 3;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void OnEnable() {
        EventManager.instance.AddListener<EvPlayerHealthChanged>(OnPlayerHealthChange);
    }

    void Start() {
        foreach (Transform child in heartContainer.transform) {
            Destroy(child.gameObject);
        }
        
        InitRender();
    }

    void OnDisable() {
        EventManager.instance.RemoveListener<EvPlayerHealthChanged>(OnPlayerHealthChange);
    }

    void InitRender() {
        for (int i = 0; i < _healthMax; i++) {
            Image heart = Instantiate(robotHeartPrefab.gameObject, heartContainer.transform).GetComponent<Image>();
            heartPrefabs.Add(heart);
            if (_health > i) {
                heart.sprite = fullHeartSprite;
            }
            else {
                heart.sprite = emptyHeartSprite;
            }
        }
    }

    void OnPlayerHealthChange(EvPlayerHealthChanged e) {
        if (e.HealthMax != _healthMax) {
            OnHealthMaxChange(e.HealthMax);
        }
        
        if (_health != e.Health) {
            OnHealthChange(e.Health);
        }
    }

    void OnHealthChange(int health) {
        _health = health;
        for (int i = 0; i < heartPrefabs.Count; i++) {
            if (_health >= i + 1) {
                heartPrefabs[i].sprite = fullHeartSprite;
            }
            else {
                heartPrefabs[i].sprite = emptyHeartSprite;
            }
        }
    }

    void OnHealthMaxChange(int healthMax) {
        if (healthMax != _healthMax) {
            if (healthMax > _healthMax) {
                for (int i = 0; i < _healthMax; i++) {
                    Image heart = Instantiate(robotHeartPrefab.gameObject, heartContainer.transform).GetComponent<Image>();
                    heartPrefabs.Add(heart);
                }
            }
            else {
                for (int i = 0; i < _healthMax - healthMax; i++) {
                    Image heartToRemove = heartPrefabs[^1];
                    heartPrefabs.Remove(heartToRemove);
                    Destroy(heartToRemove.gameObject);
                }
            }

            _healthMax = healthMax;
            OnHealthChange(_health);
        }
    }
}
