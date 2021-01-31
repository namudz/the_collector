using Game.Level;

namespace Services
{
    public interface IDataPersistence
    {
        LevelLeaderboard GetLevelLeaderboard(string levelId);
        void SaveLevelLeaderboard(LevelLeaderboard levelLeaderboard);
    }
}