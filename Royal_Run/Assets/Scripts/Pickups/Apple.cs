using UnityEngine;

public class Apple : Pickup
{
    const float AdjustChangeMoveSpeedAmount = 2f;
    private LevelGenerator _levelGenerator;
    
    private void Start()
    {
        _levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    protected override void OnPickup()
    {
        _levelGenerator.ChangeChunkMoveSpeed(AdjustChangeMoveSpeedAmount);
        Debug.Log("Apple picked up");
    }
}