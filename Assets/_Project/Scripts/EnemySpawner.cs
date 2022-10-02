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
    
    List<Vector3> spawnPositions = new();

    float _tempSpawnMax = 1f;
    float _tempSpawnTimer = 1f;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    
    void Start() {
        foreach (Transform t in spawnTransforms) {
            spawnPositions.Add(t.position);
        }

        Transform trans = transform;
        WolfPool = new ObjectPool<Enemy>(wolfPrefab, 10, trans);
        XpPool = new ObjectPool<XpDrop>(xpPrefab, 10, trans);
    }

    void Update() {
        if (GameManager.Instance.isGameOver) return;
        _tempSpawnTimer -= Time.deltaTime;
        if (_tempSpawnTimer <= 0) {
            SpawnWolfRandom();
            _tempSpawnTimer = _tempSpawnMax;
        }
    }

    void SpawnWolfRandom() {
        Enemy e = WolfPool.Get();
        e.transform.position = RandomUtils.GetRandomItem(spawnPositions);
        e.gameObject.SetActive(true);
        e.Init();
    }
}
