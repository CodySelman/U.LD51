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

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            Debug.Log("xp pickup");
            EventManager.instance.Raise(_e);
            EnemySpawner.Instance.XpPool.Return(this);
        }
    }
}
