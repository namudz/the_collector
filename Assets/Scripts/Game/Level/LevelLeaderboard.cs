using System;
using System.Collections.Generic;

namespace Game.Level
{
    [Serializable]
    public class LevelLeaderboard
    {
        public string LevelId;
        public List<LeaderboardEntry> Entries;
    }

    [Serializable]
    public class LeaderboardEntry
    {
        public int Position;
        public string UserName;
        public int Score;
    }
}