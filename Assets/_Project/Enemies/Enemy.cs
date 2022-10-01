using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // components
    [SerializeField] TopdownController2d topdownController;
    
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
        
        if (playerDist < shipDist) {
            if (playerDist <= attackRange) {
                // attack
            }
            else {
                Vector2 inputVec = new Vector2(playerPos.x, playerPos.y) - new Vector2(pos.x, pos.y).normalized;
                Move(inputVec);
            }
        }
        else {
            if (shipDist <= attackRange) {
                // attack
            }
            else {
                Vector2 inputVec = new Vector2(shipPos.x, shipPos.y) - new Vector2(pos.x, pos.y).normalized;
                Move(inputVec);
            }
        }
    }

    void Move(Vector2 inputVec) {
        // inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector2 moveVec = inputVec * (moveSpeed * Time.deltaTime);
        topdownController.Move(moveVec, inputVec);
    }
}
