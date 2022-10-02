using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodTools.Utilities;

public class UpgradeManager : MonoBehaviour
{
    public UpgradeManager Instance;
    
    [SerializeField] TMP_Text xPText;

    [SerializeField] int xpIncreaseRate = 3;

    int _level = 1;
    int _xpRequiredForLevel = 3;
    int _currentXp = 0;
    
    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void OnEnable() {
        EventManager.instance.AddListener<EvXpPickedUp>(OnXpPickedUp);
    }

    void Start() {
        SetXpText();
    }

    void OnDisable() {
        EventManager.instance.RemoveListener<EvXpPickedUp>(OnXpPickedUp);
    }

    void OnXpPickedUp(EvXpPickedUp e) {
        _currentXp++;
        SetXpText();
        if (_currentXp >= _xpRequiredForLevel) {
            LevelUp();
        }
    }

    void LevelUp() {
        _level++;
        _currentXp = 0;
        _xpRequiredForLevel = _level * xpIncreaseRate;
        SetXpText();
        // open upgrade panel, display upgrades
    }

    void SetXpText() {
        xPText.text = $"{_currentXp}/{_xpRequiredForLevel}";
    }
}
