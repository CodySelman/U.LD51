using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource shoot;
    [SerializeField] AudioSource reload;
    [SerializeField] AudioSource playerHurt;
    [SerializeField] AudioSource baseHurt;
    [SerializeField] AudioSource enemyHurt;
    [SerializeField] AudioSource enemyDead;
    [SerializeField] AudioSource levelUp;
    [SerializeField] AudioSource gameLost;
    [SerializeField] AudioSource gameWin;
    [SerializeField] AudioSource xpPickup;
    
    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
            GameObject.DontDestroyOnLoad(this);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void Shoot() {
        shoot.Play();
    }

    public void Reload() {
        reload.Play();
    }

    public void PlayerHurt() {
        playerHurt.Play();
    }

    public void BaseHurt() {
        baseHurt.Play();
    }

    public void EnemyHurt() {
        enemyHurt.Play();
    }

    public void EnemyDead() {
        enemyDead.Play();
    }

    public void LevelUp() {
        levelUp.Play();
    }

    public void GameLost() {
        gameLost.Play();
    }

    public void GameWon() {
        gameWin.Play();
    }

    public void XpPickup() {
        xpPickup.Play();
    }
}
