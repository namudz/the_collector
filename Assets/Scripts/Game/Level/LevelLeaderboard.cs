using System;
using System.Collections.Generic;

namespace Game.Level
{
    [Serializable]
    public class LevelLeaderboard
    {
        public string LevelId;
        public List<LeaderboardEntry> Entries;

        public void InitializeEntries()
        {
            for (var i = 0; i < Entries.Capacity; ++i)
            {
                var entry = new LeaderboardEntry
                {
                    Position = i + 1,
                    Score = -1,
                    UserName = string.Empty
                };
                Entries.Add(entry);
            }
        }
    }

    [Serializable]
    public class LeaderboardEntry
    {
        public int Position;
        public string UserName;
        public int Score;
    }
}