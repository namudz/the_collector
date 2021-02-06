namespace Game
{
    public interface IGame
    {
        string CurrentLevelId { get; }
        bool HasGameStarted { get; }
        bool IsGameOver { get; }

        void SetCurrentLevelData(Level.Level level);
        void Load();
        void Tick();
        void Start(bool directStart = false);
        void Reset();
    }
}