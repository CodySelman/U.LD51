using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(Rigidbody2D)))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(TopdownController2d))]
    public class Player : MonoBehaviour
    {
        // components
        [SerializeField] Rigidbody2D rb;
        [SerializeField] BoxCollider2D collider;
        [SerializeField] TopdownController2d topdownController;

        // variables
        [SerializeField] int healthMax = 3;
        [SerializeField] int health = 3;
        [SerializeField] float moveSpeed = 5f;

        Vector2 _inputVec = Vector2.zero;

        void Start() {
            health = healthMax;
        }

        void Update() {
            _inputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            Vector2 moveVec = _inputVec * (moveSpeed * Time.deltaTime);
            topdownController.Move(moveVec, _inputVec);
        }

    }
