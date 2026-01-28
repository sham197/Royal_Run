using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private float _shakeModifier = 2f;
    [SerializeField] private ParticleSystem collisionParticleSystem;
    [SerializeField] private AudioSource boulderSmashAudio;
    [SerializeField] CinemachineImpulseSource impulseSource;
    private float _effectsCooldown = 1f;
    private float _effectsTimer;
    private bool _canUseEffects = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (!_canUseEffects) return;
        CalculateShakeIntensity();
        CollisionFX(collision);
        _effectsTimer = _effectsCooldown;
    }
    
    private void Update()
    {
        if (!_canUseEffects) _effectsTimer -= Time.deltaTime;
        _canUseEffects = _effectsTimer <= 0;
    }
    
    void CalculateShakeIntensity()
    {
        var distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        var shakeIntensity = (1f / distance) * _shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        impulseSource.GenerateImpulse();
    }

    private void CollisionFX(Collision collision)
    {
        var contactPoint = collision.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        boulderSmashAudio.Play();
    }
}