using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // components
    [SerializeField] TopdownController2d topdownController;
    [SerializeField] SpriteRenderer sr;
    
    // variables
    [SerializeField] int healthMax = 3;
    [SerializeField] int health = 3;
    [SerializeField] float attackRange;
    [SerializeField] float moveSpeed = 4f;

    Player _player;
    Ship _ship;
    Transform _playerTransform;
    Transform _shipTransform;

    void Start() {
        health = healthMax;
        
        _player ??= GameManager.Instance.player;
        _playerTransform ??= _player.transform;
        _ship ??= GameManager.Instance.ship;
        _shipTransform ??= _ship.transform;
    }

    void Update() {
        Vector3 pos = transform.position;
        Vector3 playerPos = _playerTransform.position;
        Vector3 shipPos = _shipTransform.position;
        float playerDist = Vector3.Distance(pos, playerPos);
        float shipDist = Vector3.Distance(pos, shipPos);
        
        if (playerDist < shipDist && _player.isAlive) {
            if (playerPos.x > pos.x) {
                sr.flipX = false;
            }
            else {
                sr.flipX = true;
            }
            if (playerDist <= attackRange) {
                // attack
            }
            else {
                Vector2 inputVec = (new Vector2(playerPos.x, playerPos.y) - new Vector2(pos.x, pos.y)).normalized;
                Move(inputVec);
            }
        }
        else {
            if (shipPos.x > pos.x) {
                sr.flipX = false;
            }
            else {
                sr.flipX = true;
            }
            if (shipDist <= attackRange) {
                // attack
            }
            else {
                Vector2 inputVec = (new Vector2(shipPos.x, shipPos.y) - new Vector2(pos.x, pos.y)).normalized;
                Move(inputVec);
            }
        }
    }

    void OnCollisionStay2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            Player p = col.gameObject.GetComponent<Player>();
            p.GetHit();
        } else if (col.gameObject.CompareTag("Base")) {
            Ship.Instance.GetHit();
        }
    }

    void Move(Vector2 inputVec) {
        Vector2 moveVec = inputVec * (moveSpeed * Time.deltaTime);
        topdownController.Move(moveVec, inputVec);
    }

    public void GetHit(int damage) {
        // TODO animation
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        // TODO pooling
        // TODO animation
        gameObject.SetActive(false);
    }
}
