using SceneLoader;

public interface IGame
{
    SceneConstants.Mazes CurrentLevel { get; }
    void Load();
    void Tick();
    void Start();
    void Reset();
}