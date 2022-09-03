namespace Game
{
    public interface IGame
    {
        string CurrentLevelId { get; }
        bool HasGameStarted { get; }
        bool IsGameOver { get; }

        void SetCurrentLevelData(DomainLayer.Level level);
        void Load();
        void Tick();
        void GetReady();
        void Start();
        void Reset();
    }
}