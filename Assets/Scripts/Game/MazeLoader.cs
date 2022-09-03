using System;
using Game;
using InterfaceAdapters.Services.SceneLoader;

public class MazeLoader : IMazeLoader
{
    private readonly ISceneLoader _sceneLoader;

    public MazeLoader(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Load(string mazeSceneName, Action onComplete)
    {
        _sceneLoader.LoadMazeScene(mazeSceneName, onComplete);
    }
}