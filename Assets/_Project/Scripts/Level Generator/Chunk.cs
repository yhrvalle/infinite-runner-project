using System.Collections.Generic;
using UnityEngine;
public class Chunk : MonoBehaviour
{
    private const float LeftLane = -3f;
    private const float RightLane = 3f;
    private const float MiddleLane = 0f;

    [SerializeField] private float[] lanesPosition = { LeftLane, MiddleLane, RightLane };
    [SerializeField] private GameObject fencePrefab;
    [SerializeField] private GameObject applePrefab;
    private readonly List<int> _availableLanes = new() { 0, 1, 2 };

    private void Start()
    {
        SpawnFence();
    }
    private void SpawnFence()
    {
        int fencesToSpawn = Random.Range(0, _availableLanes.Count); // number of fences in a chunk
        for (int i = 0; i < fencesToSpawn; i++)
        {
            SpawnAtSelectedLane(fencePrefab);
        }
    }

    private void SpawnApple()
    {
        SpawnAtSelectedLane(applePrefab);
    }

    private void SpawnAtSelectedLane(GameObject prefab)
    {
        int selectedLane = SelectLane();
        Vector3 spawnPosition = new(lanesPosition[selectedLane], transform.position.y, transform.position.z);
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
