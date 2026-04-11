using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private const float PlayerSpeedIncrease = 0.1f;
    private const float ObstacleSpeedIncrease = 0.2f;
    [SerializeField] private int timeIncrease;
    private GameManager _gameManager;
    private PlayerMovement _playerMovement;
    private ObstacleSpawner  _obstacleSpawner;

    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _playerMovement = FindAnyObjectByType<PlayerMovement>();
        _obstacleSpawner = FindAnyObjectByType<ObstacleSpawner>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        _gameManager.IncreaseTimeLeft(timeIncrease);
        _obstacleSpawner.DecreaseSpawnRate(ObstacleSpeedIncrease);
        _playerMovement.IncreaseMoveSpeed(PlayerSpeedIncrease);
    }
}
