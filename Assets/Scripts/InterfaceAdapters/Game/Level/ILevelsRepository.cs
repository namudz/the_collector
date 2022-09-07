namespace InterfaceAdapters.Game.Level
{
    public interface ILevelsRepository
    {
        void AddLevel(Level level);
        int LevelsCount { get; }
        Level GetLevel(int levelIndex);
        Level GetLevel(string levelId);
    }
}