using Game.Level;

namespace Services.DataPersistence
{
    public interface IDataPersistence
    {
        LevelLeaderboard GetLevelLeaderboard(string levelId);
        void SaveLevelLeaderboard(LevelLeaderboard levelLeaderboard);
    }
}