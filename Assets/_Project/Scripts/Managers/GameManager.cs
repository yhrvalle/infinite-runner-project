using Enthalpy.Input;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInputReader playerInputReader;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private float startTime = 5f;
    private float _timeLeft;
    private bool _isGameOver;
    public bool IsGameOver => _isGameOver;

    private void Start()
    {
        _timeLeft = startTime;
    }
    
    private void Update()
    {
        GameStopwatch();
    }
    
    private void GameStopwatch()
    {
        if (_isGameOver) return;
        _timeLeft -= Time.deltaTime;
        timerText.text = _timeLeft.ToString("F1");
        if (_timeLeft <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        _isGameOver = true;
        playerInputReader.DisablePlayerInputActions();
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
    }
    
    public void IncreaseTimeLeft(float amount)
    {
        _timeLeft += amount;
    }
}
