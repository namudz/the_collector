namespace Game
{
    public interface IGame
    {
        DomainLayer.Level CurrentLevel { get; set; }
        bool HasGameStarted { get; }
        bool IsGameOver { get; }

        void GetReady();
        void Start();
        void Tick();
        void Reset();
    }
}