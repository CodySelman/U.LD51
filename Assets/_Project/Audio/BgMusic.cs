using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusic : MonoBehaviour
{
    public static BgMusic Instance;
    
    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
            GameObject.DontDestroyOnLoad(this);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
}
