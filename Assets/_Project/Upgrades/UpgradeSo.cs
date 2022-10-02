using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UpgradeSo", order = 1)]
public class UpgradeSo : ScriptableObject
{
    public UpgradeType upgradeType;
    public float amount;
    public string description;
}
