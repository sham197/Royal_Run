using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using UnityEditor.ShaderGraph.Internal;

public class CameraController : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedupParticleSystem;
    [SerializeField] private float minFOV = 40f;
    [SerializeField] private float maxFOV = 80f;
    [SerializeField] private float zoomDuration = 1f;
    [SerializeField] private float zoomSpeedModifier = 2f;
    private CinemachineCamera _cinemachineCamera;
    private void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVCoroutine(speedAmount));
        
        if(speedAmount > 0)
        {
            speedupParticleSystem.Play();
        }
    }

    private IEnumerator ChangeFOVCoroutine(float speedAmount)
    {
        var startFOV = _cinemachineCamera.Lens.FieldOfView;
        var targetFOV = Mathf.Clamp(startFOV + speedAmount * zoomSpeedModifier, minFOV, maxFOV);
        var elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;

            var t = elapsedTime / zoomDuration;
            _cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        _cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}