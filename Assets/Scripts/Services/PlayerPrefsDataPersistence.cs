using Game.Level;
using UnityEngine;

namespace Services
{
    public class PlayerPrefsDataPersistence : IDataPersistence
    {
        private const string LevelLeaderboard = "Level{0}Leaderboard";
        private readonly IJsonParser _jsonParser;

        public PlayerPrefsDataPersistence(IJsonParser jsonParser)
        {
            _jsonParser = jsonParser;
        }
        
        public LevelLeaderboard GetLevelLeaderboard(string levelId)
        {
            var key = string.Format(LevelLeaderboard, levelId);
            var profile = PlayerPrefs.GetString(key);
            return _jsonParser.FromJson<LevelLeaderboard>(profile);
        }
        
        public void SaveLevelLeaderboard(LevelLeaderboard levelLeaderboard)
        {
            var key = string.Format(LevelLeaderboard, levelLeaderboard.LevelId);
            PlayerPrefs.SetString(key, _jsonParser.ToJson(levelLeaderboard));
        }
    }
}