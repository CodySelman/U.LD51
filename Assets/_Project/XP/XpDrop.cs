using System;
using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using UnityEngine;

public class XpDrop : MonoBehaviour
{
    EvXpPickedUp _e;
    void Start() {
        _e = new EvXpPickedUp();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            EventManager.instance.Raise(_e);
            EnemySpawner.Instance.XpPool.Return(this);
        }
    }
}
