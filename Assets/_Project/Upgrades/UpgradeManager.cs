using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodTools.Utilities;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;
    
    [SerializeField] TMP_Text xPText;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] TMP_Text upgradeButtonOneText;
    [SerializeField] TMP_Text upgradeButtonTwoText;
    [SerializeField] TMP_Text upgradeButtonThreeText;
    [SerializeField] List<UpgradeSo> upgrades;

    [SerializeField] int xpIncreaseRate = 3;

    public bool isUpgrading = false;

    int _level = 1;
    int _xpRequiredForLevel = 3;
    int _currentXp = 0;
    List<UpgradeSo> _currentUpgrades = new();
    
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
        upgradePanel.SetActive(false);
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

    public void UpgradeButtonOneClick() {
        ApplyUpgrade(_currentUpgrades[0]);
    }
    
    public void UpgradeButtonTwoClick() {
        ApplyUpgrade(_currentUpgrades[1]);
    }
    
    public void UpgradeButtonThreeClick() {
        ApplyUpgrade(_currentUpgrades[2]);
    }

    public void ApplyUpgrade(UpgradeSo upgrade) {
        switch (upgrade.upgradeType) {
            case UpgradeType.None:
                Debug.LogError($"Bad UpgradeType passed: {upgrade.upgradeType}");
                break;
            case UpgradeType.BulletDamage:
                GameManager.Instance.player.gun.ChangeDamageMod(upgrade.amount);
                break;
            case UpgradeType.BulletSize:
                GameManager.Instance.player.gun.ChangeSizeMod(upgrade.amount);
                break;
            case UpgradeType.BulletSpeed:
                GameManager.Instance.player.gun.ChangeSpeedMod(upgrade.amount);
                break;
            case UpgradeType.BulletSpread:
                GameManager.Instance.player.gun.ChangeSpreadMod(upgrade.amount);
                break;
            case UpgradeType.ClipSize:
                GameManager.Instance.player.gun.ChangeClipSizeMod(upgrade.amount);
                break;
            case UpgradeType.FireRate:
                GameManager.Instance.player.gun.ChangeFireRateMod(upgrade.amount);
                break;
            case UpgradeType.ReloadTime:
                GameManager.Instance.player.gun.ChangeReloadTimeMod(upgrade.amount);
                break;
            case UpgradeType.PlayerMoveSpeed:
                GameManager.Instance.player.ChangeMoveSpeedMod(upgrade.amount);
                break;
            case UpgradeType.ShipHealth:
                GameManager.Instance.ship.RecoverHealth(Mathf.FloorToInt(upgrade.amount));
                break;
            case UpgradeType.ShipHealthMax:
                GameManager.Instance.ship.IncreaseHealthMax(Mathf.FloorToInt(upgrade.amount));
                break;
            default:
                Debug.LogError($"Bad UpgradeType passed: {upgrade.upgradeType}");
                break;
        }
        
        upgradePanel.SetActive(false);
        isUpgrading = false;
        Reticle.Instance.SetVisible(true);
    }

    void LevelUp() {
        _level++;
        _currentXp = 0;
        _xpRequiredForLevel = _level * xpIncreaseRate;
        SetXpText();
        isUpgrading = true;
        OpenUpgradePanel();
    }

    void OpenUpgradePanel() {
        List<UpgradeSo> shuffledUpgrades = RandomUtils.Shuffle(upgrades);
        _currentUpgrades.Clear();
        for (int i = 0; i < 3; i++) {
            _currentUpgrades.Add(shuffledUpgrades[i]);
        }
        // get 3 random upgrades
        upgradePanel.SetActive(true);
        upgradeButtonOneText.text = _currentUpgrades[0].description;
        upgradeButtonTwoText.text = _currentUpgrades[1].description;
        upgradeButtonThreeText.text = _currentUpgrades[2].description;
        
        Reticle.Instance.SetVisible(false);
    }

    void SetXpText() {
        xPText.text = $"{_currentXp}/{_xpRequiredForLevel}";
    }
}
