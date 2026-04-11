using System.Collections;
using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    private const float SpawnWidth = 4f;
    private const float ObstacleSpawnRateCap = 0.2f;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject obstacleParent;
    private float obstacleSpawnRate = 2f;

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
            yield return new  WaitForSeconds(obstacleSpawnRate);
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent.transform);
        }
    }

    public void DecreaseSpawnRate(float amount)
    {
        obstacleSpawnRate = Mathf.Max(ObstacleSpawnRateCap, obstacleSpawnRate - amount);
    }
        
        
}
