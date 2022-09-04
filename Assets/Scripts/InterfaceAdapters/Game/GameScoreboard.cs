using System;
using UnityEngine;

namespace InterfaceAdapters.Game
{
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
            LaunchScoreUpdatedEvent();
        }

        public void AddScore(float percIncrement)
        {
            CurrentScore += Mathf.RoundToInt(CurrentScore * percIncrement);
        }

        public void Reset()
        {
            CurrentScore = 0;
            LaunchScoreUpdatedEvent();
        }

        private void LaunchScoreUpdatedEvent()
        {
            OnScoreUpdated?.Invoke(CurrentScore);
        }
    }
}