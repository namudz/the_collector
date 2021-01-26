using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Game : IGame
{
    private ISpawner _heroSpawner;
    private IEnumerable<ISpawner> _collectiblesSpawners;

    public Game()
    {
    }
    
    public void InjectSpawners(ISpawner heroSpawner, IEnumerable<ISpawner> collectibleSpawners)
    {
        _heroSpawner = heroSpawner;
        _collectiblesSpawners = collectibleSpawners;
    }

    public void Load()
    {
        Debug.LogError("TODO - Load game (maze from scriptable object / additive scene?)");
        // TODO - Once finish, call to start
        Debug.Log("Simulating loading the game 2s...");
    }
    
    public void Start()
    {
        Debug.Log("Start the game!!!");
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