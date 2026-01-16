using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeparationLength = 2f;

    [SerializeField] float[] lanes = { -2.7f, -0.3f, 2.3f };
    List<int> _availableLanes = new List<int> { 0, 1, 2 };

    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    private void SpawnFences()
    {
        var fencesToSpawn = Random.Range(0, lanes.Length);

        for (var i = 0; i < fencesToSpawn; i++)
        {
            if (_availableLanes.Count <= 0) break;

            var selectedLaneIndex = SelectLaneIndex();

            var spawnPosition = new Vector3(lanes[selectedLaneIndex], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    private void SpawnApple()
    {
        if (Random.value > appleSpawnChance || _availableLanes.Count <= 0) return;
        var selectedLaneIndex = SelectLaneIndex();

        var spawnPosition = new Vector3(lanes[selectedLaneIndex], transform.position.y, transform.position.z);
        Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform);
    }

    private void SpawnCoins()
    {
        if (Random.value > coinSpawnChance || _availableLanes.Count <= 0) return;

        var selectedLaneIndex = SelectLaneIndex();
        const int maxCoinsInPack = 5 + 1;
        var numOfCoinsInPack = Random.Range(1, maxCoinsInPack);
        var topOfChunksZPos = transform.position.z + (coinSeparationLength * 2f);
        for (var i = 0; i < numOfCoinsInPack; i++)
        {
            var spawnPositionZ = topOfChunksZPos - (i * coinSeparationLength);
            var spawnPosition = new Vector3(lanes[selectedLaneIndex], transform.position.y, spawnPositionZ);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    private int SelectLaneIndex()
    {
        var randomLaneIndex = Random.Range(0, _availableLanes.Count);
        var selectedLaneIndex = _availableLanes[randomLaneIndex];
        _availableLanes.RemoveAt(randomLaneIndex);
        return selectedLaneIndex;
    }
}