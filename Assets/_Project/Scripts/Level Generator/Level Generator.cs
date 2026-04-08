using System.Collections.Generic;
using UnityEngine;
public class LevelGenerator : MonoBehaviour
{
    private const int BridgeSize = 12;
    private const float ChunkSize = 10f;
    private const int CheckpointChunkInterval = 8;

    [Header("References")]
    [SerializeField] private GameObject[] chunkPrefabs;
    [SerializeField] private GameObject checkpointChunkPrefab;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private CameraController cameraController;
    private Camera _camera;
    private ScoreManager _scoreManager;

    [Header("Movement Settings")] 
    [SerializeField] private float minGravityZ = -2f;
    [SerializeField] private float maxGravityZ = -22f;
    [SerializeField] private float minMoveSpeed = 4f;
    [SerializeField] private float maxMoveSpeed = 16f;
    [SerializeField] private float moveSpeed = 8f;

    private readonly List<GameObject> _chunks = new();
    private int _chunkSpawned;

    private void Awake()
    {
        _scoreManager = FindAnyObjectByType<ScoreManager>();
        _camera = Camera.main;
    }
    
    private void Start()
    {
        SpawnStartingChunks();
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
    
        GameObject chunkToSpawn = ChooseChunkToSpawn();
        GameObject newChunkGo = Instantiate(chunkToSpawn, newChunkPos, transform.rotation, chunkParent);
        _chunks.Add(newChunkGo);
        Chunk newChunk = newChunkGo.GetComponent<Chunk>();
        newChunk.Init(this, _scoreManager);
        _chunkSpawned++;
    }
    
    private GameObject ChooseChunkToSpawn()
    {

        GameObject chunkToSpawn;
        if (_chunkSpawned % CheckpointChunkInterval == 0 && _chunkSpawned != 0)
        {
            chunkToSpawn = checkpointChunkPrefab;
        }
        else
        {
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        return chunkToSpawn;
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
