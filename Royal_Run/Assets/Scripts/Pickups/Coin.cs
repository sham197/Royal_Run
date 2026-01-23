using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int scoreAmount = 100;
    private ScoreManager _scoreManager;
    
    public void Init(ScoreManager scoreManager)
    {
        this._scoreManager = scoreManager;
    }
    protected override void OnPickup()
    {
        _scoreManager.UpdateScore(scoreAmount);
    }
}