using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text flavorText;

    bool _flavorTextVisible = false;

    void Start() {
        menu.gameObject.SetActive(true);
        flavorText.gameObject.SetActive(false);
    }

    void Update() {
        if (_flavorTextVisible) {
            if (
                Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.D) ||
                Input.GetMouseButtonDown(0)) {
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void StartGame() {
        _flavorTextVisible = true;
        flavorText.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }
}
