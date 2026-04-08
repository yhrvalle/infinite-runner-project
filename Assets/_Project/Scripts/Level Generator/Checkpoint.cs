using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int timeIncrease;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameManager.IncreaseTimeLeft(timeIncrease);
        }
    }
}
