using System.Collections.Generic;
using UnityEngine;
public class LevelGenerator : MonoBehaviour
{
    private const int BridgeSize = 12;
    private const float ChunkSize = 10f;

    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private Transform chunkParent;

    private readonly List<GameObject> _chunks = new();
    private readonly float _moveSpeed = 5f;
    private Camera _camera;
    private void Start()
    {
        SpawnStartingChunks();
        _camera = Camera.main;

    }
    private void Update()
    {
        ChunkMovement();
    }
    private void SpawnStartingChunks()
    {
        for (int i = 0; i < BridgeSize; i++)
        {
            SpawnChunk();
        }

    }
    private void SpawnChunk()
    {
        float spawnPosZ = CalculateSpawnPosZ();
        Vector3 newChunkPos = new(transform.position.x, transform.position.y, spawnPosZ);
        GameObject newChunk = Instantiate(chunkPrefab, newChunkPos, transform.rotation, chunkParent);
        _chunks.Add(newChunk);
    }
    private float CalculateSpawnPosZ()
    {
        float spawnPosZ;
        if (_chunks.Count == 0)
        {
            spawnPosZ = transform.position.z;
        }
        else
        {
            spawnPosZ = _chunks[^1].transform.position.z + ChunkSize;
        }

        return spawnPosZ;
    }

    private void ChunkMovement()
    {
        for (int i = 0; i < _chunks.Count; i++)
        {
            GameObject chunk = _chunks[i];
            chunk.transform.Translate(-transform.forward * (_moveSpeed * Time.deltaTime));
            if (chunk.transform.position.z < _camera.transform.position.z - ChunkSize)
            {
                _chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
