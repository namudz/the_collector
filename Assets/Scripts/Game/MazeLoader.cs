using System;
using SceneLoader;

public class MazeLoader : IMazeLoader
{
    private readonly ISceneLoader _sceneLoader;

    public MazeLoader(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Load(Action onComplete)
    {
        _sceneLoader.LoadMazeScene(SceneConstants.Mazes.Level_1, onComplete);
    }
}