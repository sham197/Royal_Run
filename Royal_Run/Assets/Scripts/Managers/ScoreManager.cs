using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text scoreText;

    private int _score;

    private void Start()
    {
        UpdateVisuals();
    }

    public void UpdateScore(int amount)
    {
        if (gameManager.GameOver) return;
        
        _score += amount;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (scoreText != null)
        {
            scoreText.text = _score.ToString();
        }
    }
}