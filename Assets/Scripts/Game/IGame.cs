namespace Game
{
    public interface IGame
    {
        string CurrentLevelIndex { get; }
        bool HasGameStarted { get; }
        bool IsGameOver { get; }
    
        void Load();
        void Tick();
        void Start();
        void Reset();
    }
}