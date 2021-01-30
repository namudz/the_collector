using System;

namespace Game.Level
{
    [Serializable]
    public class Level
    {
        public string Id;
        public string DisplayName;
        public float Countdown;
        public LevelLeaderboard Leaderboard { get; }
    }
}