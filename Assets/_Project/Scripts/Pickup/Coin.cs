using UnityEngine;
public class Coin : Pickup
{
    private ScoreManager _scoreManager;
    [SerializeField] private int scoreValue = 100;
    protected override void OnPickup()
    {
        _scoreManager.ModifyScore(scoreValue);
    }

    public void Init(ScoreManager scoreManager)
    {
        this._scoreManager =  scoreManager;
    }
}
