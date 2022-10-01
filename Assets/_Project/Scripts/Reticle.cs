using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public static Reticle Instance;
    
    [SerializeField] SpriteRenderer sr;
    
    Camera _camera;
    bool _active = true;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        _camera = Camera.main;
        SetVisible(true);
    }

    void Update() {
        if (_active) {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }

    public void SetVisible(bool isVisible) {
        Cursor.visible = !isVisible;
        sr.enabled = isVisible;
        _active = isVisible;
    }
}
