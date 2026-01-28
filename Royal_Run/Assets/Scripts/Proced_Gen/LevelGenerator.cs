using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private CameraController cameraController;

    [SerializeField] private GameObject[] chunkPrefabs;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameObject checkpointChunkPrefab;
    [SerializeField] Checkpoint checkpointChunkPrefabScript;

    [Header("Level Settings")] [Tooltip("Do not change this value without reason")] 
    [SerializeField] private int startingChunksAmount = 12;

    [SerializeField] private int checkpointSpawnInterval = 20;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 20f;

    private GameObject _chunkToSpawn;
    private const float ChunkLength = 10f;
    private List<GameObject> _chunks = new List<GameObject>();
    private Camera _camera;
    private int _spawnedChunksAmount = 0;

    void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        SpawnStartingChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        moveSpeed += speedAmount;
        ApplyGravity(speedAmount);
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
        cameraController.ChangeCameraFOV(speedAmount);
    }

    private static void ApplyGravity(float speedAmount)
    {
        var newGravity = Physics.gravity + (Vector3.back * speedAmount);

        newGravity.z = Mathf.Clamp(newGravity.z, -25f, -15f);

        Physics.gravity = newGravity;
    }


    private void SpawnStartingChunks()
    {
        for (var i = 0; i < startingChunksAmount; i++)
            SpawnSingleChunk();
    }

    void SpawnSingleChunk()
    {
        var spawnPositionZ = GetSpawnPositionZ();
        var chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);

        _chunkToSpawn = ChooseChunkToSpawn();
        
        var newChunkGo = Instantiate(_chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);

        _chunks.Add(newChunkGo);
        var newChunk = newChunkGo.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);
        _spawnedChunksAmount += 1;
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;
        if (_spawnedChunksAmount % checkpointSpawnInterval == 0 && _spawnedChunksAmount != 0)
        {
            chunkToSpawn = checkpointChunkPrefab;
        }
        else
        {
            if (chunkPrefabs.Length == 1 || Random.value < 0.8f) chunkToSpawn = chunkPrefabs[0];
            else chunkToSpawn = chunkPrefabs[Random.Range(1, chunkPrefabs.Length)];
        }
        return chunkToSpawn;
    }

    private float GetSpawnPositionZ()
    {
        float spawnPositionZ;

        if (_chunks.Count == 0) spawnPositionZ = transform.position.z;
        else spawnPositionZ = _chunks[_chunks.Count - 1].transform.position.z + ChunkLength;
        return spawnPositionZ;
    }

    private void MoveChunks()
    {
        for (var i = 0; i < _chunks.Count; i++)
        {
            var chunk = _chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (_camera && chunk.transform.position.z <= _camera.transform.position.z - ChunkLength)
            {
                _chunks.Remove(chunk);
                Destroy(chunk);
                SpawnSingleChunk();
            }
        }
    }
}