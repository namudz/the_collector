using System;
using InterfaceAdapters.Services.SceneLoader;

namespace InterfaceAdapters.Game
{
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
}