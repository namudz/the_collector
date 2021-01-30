using System;
using System.Collections.Generic;

namespace Game.Level
{
    [Serializable]
    public class Level
    {
        public string Id;
        public string DisplayName;
        public float Countdown;
        public LevelLeaderboard Leaderboard { get; private set; }

        public void InitializeLevelLeaderboard()
        {
            Leaderboard = new LevelLeaderboard
            {
                Entries = new List<LeaderboardEntry>(10)
            };
            Leaderboard.InitializeEntries();
        }
    }
}