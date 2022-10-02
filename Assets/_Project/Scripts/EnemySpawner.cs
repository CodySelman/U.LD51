using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodTools.Utilities;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    
    public List<Transform> spawnTransforms = new();
    public ObjectPool<Enemy> WolfPool;
    public ObjectPool<XpDrop> XpPool;

    [SerializeField] Enemy wolfPrefab;
    [SerializeField] XpDrop xpPrefab;

    [SerializeField] float spawnDistance = 35f;
    [SerializeField] float spawnRateIncreaseRate = 0.1f;
    [SerializeField] float spawnRateIncreaseTime = 5f;
    [SerializeField] float spawnRateInitial = 3f;
    
    List<Vector3> spawnPositions = new();

    [SerializeField] float spawnRate;
    [SerializeField] float spawnRateIncreaseTimer;
    [SerializeField] float spawnTimer;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    
    void Start() {
        spawnRate = spawnRateInitial;
        spawnRateIncreaseTimer = spawnRateIncreaseTime;
        spawnTimer = spawnRate;
        
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
        spawnTimer -= delta;
        spawnRateIncreaseTimer -= delta;
        
        if (spawnTimer <= 0) {
            SpawnWolfRandom();
            spawnTimer = spawnRate;
        }

        if (spawnRateIncreaseTimer <= 0) {
            spawnRate -= spawnRateIncreaseRate;
            spawnRateIncreaseTimer = spawnRateIncreaseTime;
        }
    }

    void SpawnWolfRandom() {
        Enemy e = WolfPool.Get();
        // e.transform.position = RandomUtils.GetRandomItem(spawnPositions);
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
        Vector3 dir = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
        Vector3 pos = dir * spawnDistance;
        e.transform.position = pos;
        e.gameObject.SetActive(true);
        e.Init();
    }
}
