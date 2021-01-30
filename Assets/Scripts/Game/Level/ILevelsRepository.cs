namespace Game.Level
{
    public interface ILevelsRepository
    {
        void AddLevel(Level level);
        Level GetLevel(string levelId);
    }
}