using System.Collections;
using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    private const float SpawnWidth = 4f;
    [SerializeField] private GameObject[] obstaclePrefabs;
    private readonly WaitForSeconds _waitForSeconds2F = new(2f);

    private void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    private IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new(Random.Range(-SpawnWidth, SpawnWidth), transform.position.y, transform.position.z);
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation);
            yield return _waitForSeconds2F;
        }
    }
}
