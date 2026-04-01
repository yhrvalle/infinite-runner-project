using System.Collections.Generic;
using UnityEngine;
public class LevelGenerator : MonoBehaviour
{
    private const int BridgeSize = 12;
    private const float ChunkSize = 10f;

    [Header("References")]
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private CameraController cameraController;
    private Camera _camera;

    [Header("Movement Settings")] 
    [SerializeField] private float minGravityZ = -2f;
    [SerializeField] private float maxGravityZ = -22f;
    [SerializeField] private float minMoveSpeed = 4f;
    [SerializeField] private float maxMoveSpeed = 16f;
    [SerializeField] private float moveSpeed = 8f;

    private readonly List<GameObject> _chunks = new();

    private void Start()
    {
        SpawnStartingChunks();
        _camera = Camera.main;

    }
    private void Update()
    {
        ChunkMovement();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        float desiredMoveSpeed = speedAmount + moveSpeed;
        desiredMoveSpeed = Mathf.Clamp(desiredMoveSpeed, minMoveSpeed, maxMoveSpeed);
        if (Mathf.Approximately(desiredMoveSpeed, moveSpeed))
        {
            return;
        }

        moveSpeed = desiredMoveSpeed;
        float desiredGravityZ = Physics.gravity.z - speedAmount;
        desiredGravityZ = Mathf.Clamp(desiredGravityZ, maxGravityZ, minGravityZ);
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, desiredGravityZ);
        cameraController.ChangeCameraFOV(speedAmount);

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
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
            if (chunk.transform.position.z < _camera.transform.position.z - ChunkSize)
            {
                _chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
