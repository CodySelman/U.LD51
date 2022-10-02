using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodTools.Utilities;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    
    public List<Transform> spawnTransforms = new();
    public ObjectPool<Enemy> WolfPool;
    public ObjectPool<XpDrop> XpPool;

    [SerializeField] Enemy wolfPrefab;
    [SerializeField] XpDrop xpPrefab;

    [SerializeField] float spawnRateIncreaseRate = 0.1f;
    [SerializeField] float spawnRateIncreaseTime = 5f;
    [SerializeField] float spawnRateInitial = 3f;
    
    List<Vector3> spawnPositions = new();

    float _spawnRate;
    float _spawnRateIncreaseTimer;
    float _spawnTimer;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    
    void Start() {
        _spawnRate = spawnRateInitial;
        _spawnRateIncreaseTimer = spawnRateIncreaseTime;
        _spawnTimer = _spawnRate;
        
        foreach (Transform t in spawnTransforms) {
            spawnPositions.Add(t.position);
        }

        Transform trans = transform;
        WolfPool = new ObjectPool<Enemy>(wolfPrefab, 10, trans);
        XpPool = new ObjectPool<XpDrop>(xpPrefab, 10, trans);
    }

    void Update() {
        if (GameManager.Instance.isGameOver || UpgradeManager.Instance.isUpgrading) return;
        float delta = Time.deltaTime;
        _spawnTimer -= delta;
        _spawnRateIncreaseTimer -= delta;
        
        if (_spawnTimer <= 0) {
            SpawnWolfRandom();
            _spawnTimer = _spawnRate;
        }

        if (_spawnRateIncreaseTimer <= 0) {
            _spawnRate -= spawnRateIncreaseRate;
            _spawnRateIncreaseTimer = spawnRateIncreaseTime;
        }
    }

    void SpawnWolfRandom() {
        Enemy e = WolfPool.Get();
        e.transform.position = RandomUtils.GetRandomItem(spawnPositions);
        e.gameObject.SetActive(true);
        e.Init();
    }
}
