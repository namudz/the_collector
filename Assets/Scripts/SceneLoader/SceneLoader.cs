using System;
using System.Collections.Generic;
using EventDispatcher;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly Dictionary<SceneConstants.Scene, string> _scenes = new Dictionary<SceneConstants.Scene,string>
        {
            { SceneConstants.Scene.MainMenu, "0_MainScene" },
            { SceneConstants.Scene.Game, "1_GameScene" }
        };
        
        private readonly Dictionary<SceneConstants.Mazes, string> _mazes = new Dictionary<SceneConstants.Mazes,string>
        {
            { SceneConstants.Mazes.Level_1, "Maze_1" },
            { SceneConstants.Mazes.Level_2, "Maze_2" }
        };

        private readonly IEventDispatcher _eventDispatcher;
        private SceneConstants.Scene _sceneLoaded;

        public SceneLoader(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }
        
        public void LoadScene(SceneConstants.Scene scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            _sceneLoaded = scene;
            LoadUnityScene(_scenes[scene], mode, DispatchSceneLoadedEvent);
        }
        
        public void LoadMazeScene(SceneConstants.Mazes mazeScene, Action onComplete)
        {
            //_sceneLoaded = mazeScene;
            LoadUnityScene(_mazes[mazeScene], LoadSceneMode.Additive, onComplete);
        }

        private void LoadUnityScene(string sceneName, LoadSceneMode mode, Action onComplete)
        {
            SceneManager.sceneLoaded += (arg0, sceneMode) => { onComplete?.Invoke(); };
            SceneManager.LoadSceneAsync(sceneName, mode);
        }

        private void DispatchSceneLoadedEvent()
        {
            _eventDispatcher.Dispatch(new SceneLoadedSignal(_sceneLoaded));
        }
    }
}