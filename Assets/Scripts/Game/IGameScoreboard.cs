using System;

public interface IGameScoreboard
{
    event Action<int> OnScoreUpdated;
    int CurrentScore { get; }
    void AddScore(int score);
    void AddScore(float percIncrement);
    void Reset();
}