using System.Collections;
using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    private const int ObstacleCount = 10;
    [SerializeField] private GameObject obstaclePrefab;

    private readonly WaitForSeconds _waitForSeconds1F = new(1f);

    private void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    private IEnumerator SpawnObstacleRoutine()
    {
        int counter = 0;
        while (counter < ObstacleCount)
        {
            Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            yield return _waitForSeconds1F;
            counter++;
        }

    }
}
