using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float checkpointTimeExtension = 5f;
    private GameManager _gameManager;

    private string _playerTag = "Player";
    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_playerTag))
        {
            _gameManager.ChangeGameTimer(checkpointTimeExtension);
        }
    }
}