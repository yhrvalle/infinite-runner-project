using System.Collections.Generic;
using UnityEngine;
public class Chunk : MonoBehaviour
{
    private const float LeftLane = -3f;
    private const float RightLane = 3f;
    private const float MiddleLane = 0f;

    private const float ApplePercent = 0.3f;
    private const float CoinPercent = 0.7f;

    private const int MaxCoinsPerChunk = 5;
    private const int MinCoinsPerChunk = 1;
    private const float CoinLaneSpacing = 2f;

    [SerializeField] private float[] lanesPosition = { LeftLane, MiddleLane, RightLane };

    [SerializeField] private GameObject fencePrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject coinPrefab;

    private readonly List<int> _availableLanes = new() { 0, 1, 2 };

    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }
    private void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanesPosition.Length); // number of fences in a chunk

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (_availableLanes.Count <= 0)
            {
                break;
            }

            int selectedLane = SelectLane();
            SpawnAtSelectedLane(fencePrefab, selectedLane);
        }
    }

    private void SpawnApple()
    {
        if (Random.value > ApplePercent || _availableLanes.Count <= 0)
        {
            return;
        }

        int selectedLane = SelectLane();
        SpawnAtSelectedLane(applePrefab, selectedLane);
    }

    private void SpawnCoins()
    {
        if (Random.value > CoinPercent || _availableLanes.Count <= 0)
        {
            return;
        }

        int selectedLane = SelectLane();
        float topOfChunk = transform.position.z + CoinLaneSpacing * 2f;
        int coinsPerChunk = Random.Range(MinCoinsPerChunk, MaxCoinsPerChunk); // 1 - 5
        for (int i = 0; i < coinsPerChunk; i++)
        {
            if (_availableLanes.Count <= 0)
            {
                break;
            }

            SpawnAtSelectedLane(coinPrefab, selectedLane, i, topOfChunk);
        }
    }

    private void SpawnAtSelectedLane(GameObject prefab, int selectedLane)
    {

        Vector3 spawnPosition = new(lanesPosition[selectedLane], transform.position.y, transform.position.z);
        Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
    }

    private void SpawnAtSelectedLane(GameObject prefab, int selectedLane, int iterator, float topOfChunkZ)
    {

        Vector3 spawnPosition = new(lanesPosition[selectedLane], transform.position.y, topOfChunkZ - iterator * CoinLaneSpacing);
        Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
    }

    private int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, _availableLanes.Count); // random position in a chunk
        int selectedLane = _availableLanes[randomLaneIndex];
        _availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }
}
