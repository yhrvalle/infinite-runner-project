using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private float startTime = 5f;
    private float _timeLeft;

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
        _timeLeft -= Time.deltaTime;
        timerText.text = _timeLeft.ToString("F1");
        if (_timeLeft <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
    }
    
}
