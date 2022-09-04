namespace InterfaceAdapters.Game.Level
{
    public interface ILevelsRepository
    {
        void AddLevel(Level level);
        Level GetLevel(int levelIndex);
        Level GetLevel(string levelId);
    }
}