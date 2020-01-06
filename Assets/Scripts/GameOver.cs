using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static GameOver SharedInstance;
    public GameObject gameOverScreen;
    public GameObject counter;
    public Text secondsUI;
    public PlayerController player;
    private bool gameOver = false;

    public bool IsGameOver {
        get {
            return gameOver;
        }
    }

    private void Awake() {
        SharedInstance = this;
    }

    private void Start() {
        FindObjectOfType<PlayerController>().OnPlayerDeath += OnGameOver;
    }

    void Update()
    {
        if (gameOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }

    private void OnGameOver() {
        gameOverScreen.SetActive(true);
        secondsUI.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
        gameOver = true;
        counter.SetActive(false);
    }
}
