using System;
using UnityEngine;

public class GameScoreboard : IGameScoreboard
{
    public event Action<int> OnScoreUpdated;
    public int CurrentScore { get; private set; }

    public GameScoreboard()
    {
        Reset();
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

    public void Reset()
    {
        CurrentScore = 0;
    }

    private void PrintScore()
    {
        OnScoreUpdated?.Invoke(CurrentScore);
    }
}