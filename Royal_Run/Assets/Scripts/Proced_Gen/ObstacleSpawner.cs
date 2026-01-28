using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] private float obstacleSpawnTime = 3f;
    [SerializeField] private float minObstacleSpawnTime = 0.2f;
    [SerializeField] private Transform obstacleParent;
    [SerializeField] private float spawnWidth = 4f;

    private void Start()
    {
        StartCoroutine(SpawnObstacleCoroutine());
    }

    public void DecreaseSpawnTime(float amount)
    {
        obstacleSpawnTime -= amount;

        if (obstacleSpawnTime < minObstacleSpawnTime)
        {
             obstacleSpawnTime = minObstacleSpawnTime;
        }
    }

    private IEnumerator SpawnObstacleCoroutine()
    {
        while (true)
        {
            var obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            var spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y,
                transform.position.z);
            yield return new WaitForSeconds(obstacleSpawnTime);
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}