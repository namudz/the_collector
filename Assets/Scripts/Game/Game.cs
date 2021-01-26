using SceneLoader;
using UnityEngine;

public class Game : IGame
{
    private readonly IMazeLoader _mazeLoader;

    public SceneConstants.Mazes CurrentLevel { get; private set; }

    public Game(IMazeLoader mazeLoader)
    {
        _mazeLoader = mazeLoader;
    }

    public void Load()
    {
        CurrentLevel = SceneConstants.Mazes.Level_1;
        _mazeLoader.Load(CurrentLevel, Start);
        // TODO - If enough time, add Loading Canvas to show when loading the level & its fully loaded
    }
    
    public void Start()
    {
        SpawnCollectibles();
        SpawnHero();
    }
    
    public void Reset()
    {
        Debug.LogError("TODO - Implement IGame.Reset");
    }

    private void SpawnHero()
    {
        _mazeLoader.HeroSpawner.Spawn();
    }

    private void SpawnCollectibles()
    {
        return;
        foreach (var spawner in _mazeLoader.CollectibleSpawners)
        {
            spawner.Spawn();
        }
    }
}