using UnityEngine;

public class Apple : Pickup
{
    const float AdjustChangeMoveSpeedAmount = 2f;
    LevelGenerator _levelGenerator;

    public void Init(LevelGenerator levelGenerator)
    {
        this._levelGenerator = levelGenerator;
    }

    protected override void OnPickup()
    {
        _levelGenerator.ChangeChunkMoveSpeed(AdjustChangeMoveSpeedAmount);
        Debug.Log("Apple picked up");
    }
}