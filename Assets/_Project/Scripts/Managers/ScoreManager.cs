using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text scoreText;
    
    public void ModifyScore(int score)
    {
        if (gameManager.IsGameOver) return;
        _score += score;
        scoreText.text = _score.ToString();
    }
}
