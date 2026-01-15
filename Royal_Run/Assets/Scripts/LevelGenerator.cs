using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private int startingChunksAmount = 12;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private float moveSpeed = 8f;

    private float _chunkLength = 10f;
    private List<GameObject> _chunks = new List<GameObject>();

    private void Start()
    {
        SpawnStartingChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    private void SpawnStartingChunks()
    {
        for (var i = 0; i < startingChunksAmount; i++)
        {
            SpawnSingleChunk();
        }
    }

    void SpawnSingleChunk()
    {
        var spawnPositionZ = GetSpawnPositionZ();

        var chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        var newChunk = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);

        _chunks.Add(newChunk);
    }

    private float GetSpawnPositionZ()
    {
        float spawnPositionZ;

        if (_chunks.Count == 0) spawnPositionZ = transform.position.z;
        else spawnPositionZ = _chunks[_chunks.Count - 1].transform.position.z + _chunkLength;
        return spawnPositionZ;
    }

    private void MoveChunks()
    {
        for (var i = 0; i < _chunks.Count; i++)
        {
            var chunk = _chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - _chunkLength)
            {
                _chunks.Remove(chunk);
                Destroy(chunk);
                SpawnSingleChunk();
            }
        }
    }
}