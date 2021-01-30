using Game.Level;

namespace DataPersistence
{
    public interface IDataPersistence
    {
        void SaveLevelLeaderboard();
        LevelLeaderboard GetLevelLeaderboard(string levelId);

    }
}