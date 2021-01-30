using System;
using UnityEngine;

public class GameScoreboard : IGameScoreboard
{
    public event Action<int> OnScoreUpdated;
    public int CurrentScore { get; private set; }

    public GameScoreboard()
    {
        CurrentScore = 0;
    }
    
    public void AddScore(int score)
    {
        CurrentScore += score;
        PrintScore();
    }

    public void AddScore(float percIncrement)
    {
        CurrentScore += Mathf.RoundToInt(CurrentScore * percIncrement);
    }

    private void PrintScore()
    {
        OnScoreUpdated?.Invoke(CurrentScore);
    }
}