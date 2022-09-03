namespace Game.Level
{
    public interface ILevelsRepository
    {
        void AddLevel(DomainLayer.Level level);
        DomainLayer.Level GetLevel(int levelIndex);
        DomainLayer.Level GetLevel(string levelId);
    }
}