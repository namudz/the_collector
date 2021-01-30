using System;
using System.Collections.Generic;
using SceneLoader;

public interface IMazeLoader
{
    ISpawner HeroSpawner { get; }
    IEnumerable<ISpawner> CollectibleSpawners { get; }
    void Load(SceneConstants.Mazes mazeToLoad, Action onComplete);
}