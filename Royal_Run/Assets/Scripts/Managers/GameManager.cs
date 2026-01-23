using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private float startTime = 5f;

    private float _timeLeft;
    private bool _gameOver;
    private void Start()
    {
        _timeLeft = startTime;
    }
    
    private void Update()
    {
        UpdateGameTimer();
    }

    void UpdateGameTimer()
    {
        if(_gameOver) return;
        
        _timeLeft -= Time.deltaTime;
        timeText.text = _timeLeft.ToString("0.0");
        
        if(_timeLeft <= 0) GameOver();
    }

    private void GameOver()
    {
        _gameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
    }
}