using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotHealthUI : MonoBehaviour
{
    public static RobotHealthUI Instance;

    [SerializeField] Image robotHeartPrefab;
    [SerializeField] Sprite fullHeartSprite;
    [SerializeField] Sprite emptyHeartSprite;

    [SerializeField] int healthMax = 3;
    [SerializeField] int health = 3;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        
    }
    
    // render health stuff out
}
