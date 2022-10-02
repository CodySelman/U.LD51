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

    void OnEnable() {
        EventManager.instance.AddListener<EvAmmoChanged>(OnAmmoChanged);
    }

    void OnDisable() {
        EventManager.instance.RemoveListener<EvAmmoChanged>(OnAmmoChanged);
    }

    void OnAmmoChanged(EvAmmoChanged e) {
        bulletText.text = $"{e.Ammo}/{e.AmmoMax}";
    }
}
