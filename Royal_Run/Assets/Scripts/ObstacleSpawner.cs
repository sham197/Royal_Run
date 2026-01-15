using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] private float obstacleSpawnTime = 3f;
    
    private void Start()
    {
        StartCoroutine(SpawnObstacleCoroutine());
    }
    
    private IEnumerator SpawnObstacleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(obstacleSpawnTime);
            Instantiate(obstaclePrefab, transform.position, Random.rotation);
        }
    }
}