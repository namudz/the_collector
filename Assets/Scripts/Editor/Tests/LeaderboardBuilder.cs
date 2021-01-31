using System.Collections.Generic;
using Game.Level;

namespace Tests
{
    public class LeaderboardBuilder
    {
        private readonly string _levelId;
        private List<LeaderboardEntry> _entries;
        
        public LeaderboardBuilder()
        {
            _levelId = "1";
            _entries = new List<LeaderboardEntry>(10);
            
            const int maxScore = 100;
            const int entryDeltaScore = 10;
            for (int i = 0; i < _entries.Capacity; i++)
            {
                var entry = new LeaderboardEntry
                {
                    Score = maxScore - (i * entryDeltaScore),
                    UserName = $"User{i}"
                };
                _entries.Add(entry);
            }
        }

        public LeaderboardBuilder WithEntryScore(int index, int score)
        {
            _entries[index].Score = score;
            return this;
        }

        public LevelLeaderboard Build()
        {
            return new LevelLeaderboard
            {
                LevelId = _levelId,
                Entries = _entries
            };
        }
    }
}