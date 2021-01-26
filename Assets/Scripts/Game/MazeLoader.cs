﻿using System;
using System.Collections.Generic;
using SceneLoader;
using UnityEngine;

public class MazeLoader : IMazeLoader
{
    private readonly ISceneLoader _sceneLoader;
    private const string TagHeroSpawner = "HeroSpawner";

    public ISpawner HeroSpawner { get; private set; }
    public IEnumerable<ISpawner> CollectibleSpawners { get; private set; }

    private Action _onMazeLoaded;

    public MazeLoader(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Load(SceneConstants.Mazes mazeToLoad, Action onComplete)
    {
        _onMazeLoaded = onComplete;
        _sceneLoader.LoadMazeScene(mazeToLoad, FindSpawners);
    }

    private void FindSpawners()
    {
        HeroSpawner = GameObject.FindWithTag(TagHeroSpawner)?.GetComponent<ISpawner>();
        _onMazeLoaded?.Invoke();
    }
}