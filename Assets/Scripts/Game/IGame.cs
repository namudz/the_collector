using SceneLoader;

public interface IGame
{
    SceneConstants.Mazes CurrentLevel { get; }
    void Load();
    void Start();
    void Reset();
}