using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager Instance { get; set; }

    [SerializeField] private TMP_Text scoreText;

    private int _score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateVisuals();
    }

    public void UpdateScore(int amount)
    {
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