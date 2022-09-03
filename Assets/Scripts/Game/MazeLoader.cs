using System;
using Collectibles.Spawner;
using InterfaceAdapters.Services.SceneLoader;
using UnityEngine;

public class MazeLoader : IMazeLoader
{
    private readonly ISceneLoader _sceneLoader;
    private readonly ICollectiblesSpawner _collectiblesSpawner;
    
    private const string TagHeroSpawner = "HeroSpawner";
    private const string TagCollectibleSpawnPoint = "CollectibleSpawnPoint";

    private ISpawner _heroSpawner;
    private Action _onMazeLoaded;

    public MazeLoader(ISceneLoader sceneLoader, ICollectiblesSpawner collectiblesSpawner)
    {
        _sceneLoader = sceneLoader;
        _collectiblesSpawner = collectiblesSpawner;
    }

    public void Load(string mazeSceneName, Action onComplete)
    {
        _onMazeLoaded = onComplete;
        _sceneLoader.LoadMazeScene(mazeSceneName, FindSpawners);
    }

    public void SpawnElements()
    {
        _heroSpawner.Spawn();
        _collectiblesSpawner.Spawn();
    }

    public void Reset()
    {
        _collectiblesSpawner.Reset();
        _heroSpawner.Reset();
    }

    private void FindSpawners()
    {
        _heroSpawner = GameObject.FindWithTag(TagHeroSpawner)?.GetComponent<ISpawner>();
        
        var points = GameObject.FindGameObjectsWithTag(TagCollectibleSpawnPoint);
        _collectiblesSpawner.SetSpawnPoints(points);
        
        _onMazeLoaded?.Invoke();
    }
}