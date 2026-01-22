using System.Collections;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private bool _canUseHitAnim = true;
    private bool _isInvulnerable = true;

    const float CollisionCooldown = 1f;
    const float StartDelay = 1f;
    const float AdjustChangeMoveSpeedAmount = -2f;

    private static readonly int HitHash = Animator.StringToHash("Hit");
    [SerializeField] private Animator animator;

    private LevelGenerator _levelGenerator;

    private void Start()
    {
        StartCoroutine(InvulnerabilityCoroutine());
        _levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_canUseHitAnim || _isInvulnerable) return;
        
        _levelGenerator.ChangeChunkMoveSpeed(AdjustChangeMoveSpeedAmount);
        animator.SetTrigger(HitHash);
        _canUseHitAnim = false;
        StartCoroutine(HitThresholdCoroutine());
    }

    private IEnumerator HitThresholdCoroutine()
    {
        yield return new WaitForSeconds(CollisionCooldown);
        _canUseHitAnim = true;
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        yield return new WaitForSeconds(StartDelay);
        _isInvulnerable = false;
    }
}