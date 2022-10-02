using System;
using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    public Ship ship;
    [SerializeField] TMP_Text gameTimeRemainingText;

    [SerializeField] Enemy wolfPrefab;

    [SerializeField] float gameMinutesMax = 10f;

    float _gameSecondsRemaining;
    int _prevSecondsRemaining;
    
    ObjectPool<Enemy> _wolfPool;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        _gameSecondsRemaining = gameMinutesMax * 60;
        _wolfPool = new(wolfPrefab, 10, transform);
    }

    void Update() {
        _gameSecondsRemaining -= Time.deltaTime;
        SetGameTime();
        
        if (_gameSecondsRemaining <= 0f) {
            // TODO win!!!
        }
    }

    void SetGameTime() {
        TimeSpan t = TimeSpan.FromSeconds(_gameSecondsRemaining);
        string s = t.Seconds < 10 ? $"0{t.Seconds}" : t.Seconds.ToString();
        string time = $"{t.Minutes}:{s}";
        gameTimeRemainingText.text = time;
    }
}
