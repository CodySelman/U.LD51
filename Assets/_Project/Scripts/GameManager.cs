using System;
using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    public Ship ship;
    public bool isGameOver = false;
    [SerializeField] TMP_Text gameTimeRemainingText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text gameOverHeading;
    [SerializeField] TMP_Text gameOverBody;

    [SerializeField] float gameMinutesMax = 10f;

    const string GameLoseHeading = "Mission Failure";
    const string GameLoseBody = "Probability of repair: 0%. Hostile native life proved to be insurmountable. Initiating self-destruct. Mission Control has offered the following choices for next mission:";
    const string GameWinHeading = "Mission Success";
    const string GameWinBody = "Sufficient material gathered to fully repair ship. Engaging weapon systems to eradicate native life. Mission control has offered the following choices for next mission:";
    float _gameSecondsRemaining;
    int _prevSecondsRemaining;

    void Awake() {
        // singleton setup
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        gameOverPanel.SetActive(false);
        _gameSecondsRemaining = gameMinutesMax * 60;
    }

    void Update() {
        if (isGameOver || UpgradeManager.Instance.isUpgrading) return;
        _gameSecondsRemaining -= Time.deltaTime;
        SetGameTime();
        
        if (_gameSecondsRemaining <= 0f) {
            GameWin();
        }
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Restart() {
        SceneManager.LoadScene("Game");
    }

    public void GameOver() {
        isGameOver = true;
        Reticle.Instance.SetVisible(false);
        gameOverPanel.SetActive(true);
        gameOverHeading.text = GameLoseHeading;
        gameOverBody.text = GameLoseBody;
    }

    void GameWin() {
        isGameOver = true;
        Reticle.Instance.SetVisible(false);
        gameOverPanel.SetActive(true);
        gameOverHeading.text = GameWinHeading;
        gameOverBody.text = GameWinBody;
    }

    void SetGameTime() {
        TimeSpan t = TimeSpan.FromSeconds(_gameSecondsRemaining);
        string s = t.Seconds < 10 ? $"0{t.Seconds}" : t.Seconds.ToString();
        string time = $"{t.Minutes}:{s}";
        gameTimeRemainingText.text = time;
    }
}
