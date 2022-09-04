using InterfaceAdapters.Game.Level;

namespace InterfaceAdapters.Services.DataPersistence
{
    public interface IDataPersistence
    {
        LevelLeaderboard GetLevelLeaderboard(string levelId);
        void SaveLevelLeaderboard(LevelLeaderboard levelLeaderboard);
    }
}