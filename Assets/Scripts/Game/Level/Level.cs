using System;
using System.Collections.Generic;

namespace Game.Level
{
    [Serializable]
    public class Level
    {
        public string Id;
        public string DisplayName;
        public string SceneName => $"Maze_{Id}";
        public float Countdown;
        public LevelLeaderboard Leaderboard { get; set; }

        public void InitializeLevelLeaderboard(string levelId)
        {
            Leaderboard = new LevelLeaderboard
            {
                LevelId = levelId,
                Entries = new List<LeaderboardEntry>(10)
            };
            Leaderboard.InitializeEntries();
        }
    }
}