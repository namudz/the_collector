namespace InterfaceAdapters.Game
{
    public interface IGame
    {
        Level.Level CurrentLevel { get; set; }
        bool HasGameStarted { get; }
        bool IsGameOver { get; }

        void GetReady();
        void Start();
        void Tick();
        void Reset();
    }
}