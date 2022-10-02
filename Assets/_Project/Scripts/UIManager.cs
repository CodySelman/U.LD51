using System;
using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text bulletText;
    [SerializeField] TMP_Text batteryText;
    [SerializeField] TMP_Text shipHealthText;

    void OnEnable() {
        EventManager.instance.AddListener<EvAmmoChanged>(OnAmmoChanged);
        EventManager.instance.AddListener<EvBatteryChanged>(OnBatteryChanged);
        EventManager.instance.AddListener<EvShipHealthChanged>(OnShipHealthChanged);
    }

    void OnDisable() {
        EventManager.instance.RemoveListener<EvAmmoChanged>(OnAmmoChanged);
        EventManager.instance.RemoveListener<EvBatteryChanged>(OnBatteryChanged);
        EventManager.instance.RemoveListener<EvShipHealthChanged>(OnShipHealthChanged);
    }

    void OnAmmoChanged(EvAmmoChanged e) {
        bulletText.text = $"{e.Ammo}/{e.AmmoMax}";
    }

    void OnBatteryChanged(EvBatteryChanged e) {
        batteryText.text = e.SecondsLeft.ToString();
    }

    void OnShipHealthChanged(EvShipHealthChanged e) {
        shipHealthText.text = $"{e.Health}/{e.HealthMax}";
    }
}
