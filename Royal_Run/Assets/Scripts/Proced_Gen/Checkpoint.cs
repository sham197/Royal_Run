using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float checkpointTimeExtension = 5f;
    [SerializeField] private float obstacleSpawnTimeDecrease = 0.2f;
    private GameManager _gameManager;
    private ObstacleSpawner _obstacleSpawner;

    private string _playerTag = "Player";
    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_playerTag))
        {
            _gameManager.ChangeGameTimer(checkpointTimeExtension);
            _obstacleSpawner.DecreaseSpawnTime(obstacleSpawnTimeDecrease);
        }
    }
}