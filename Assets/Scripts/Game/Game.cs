using System.Collections.Generic;
using UnityEngine;

public class Game : IGame
{
    private readonly IMazeLoader _mazeLoader;
    private ISpawner _heroSpawner;
    private IEnumerable<ISpawner> _collectiblesSpawners;

    public Game(IMazeLoader mazeLoader)
    {
        _mazeLoader = mazeLoader;
    }
    
    public void InjectSpawners(ISpawner heroSpawner, IEnumerable<ISpawner> collectibleSpawners)
    {
        _heroSpawner = heroSpawner;
        _collectiblesSpawners = collectibleSpawners;
    }

    public void Load()
    {
        _mazeLoader.Load(Start);
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
        _heroSpawner.Spawn();
    }

    private void SpawnCollectibles()
    {
        foreach (var spawner in _collectiblesSpawners)
        {
            spawner.Spawn();
        }
    }
}