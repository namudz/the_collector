using SceneLoader;

namespace Game
{
    public interface IGame
    {
        SceneConstants.Mazes CurrentLevel { get; }
        bool HasGameStarted { get; }
        bool IsGameOver { get; }
    
        void Load();
        void Tick();
        void Start();
        void Reset();
    }
}