using System;
using System.Collections.Generic;
using InterfaceAdapters.Services.EventDispatcher;
using UnityEngine.SceneManagement;

namespace InterfaceAdapters.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        private readonly Dictionary<SceneConstants.Scene, string> _scenes = new Dictionary<SceneConstants.Scene,string>
        {
            { SceneConstants.Scene.Splash, "0_SplashScene" },
            { SceneConstants.Scene.MainMenu, "1_MainScene" },
            { SceneConstants.Scene.Game, "2_GameScene" }
        };

        private readonly IEventDispatcher _eventDispatcher;
        private readonly ISceneManagerAdapter _sceneManagerAdapter;
        private SceneConstants.Scene _sceneLoaded;

        public SceneLoader(IEventDispatcher eventDispatcher, ISceneManagerAdapter sceneManagerAdapter)
        {
            _eventDispatcher = eventDispatcher;
            _sceneManagerAdapter = sceneManagerAdapter;
        }

        public void LoadScene(SceneConstants.Scene scene, LoadSceneMode mode)
        {
            _sceneLoaded = scene;
            _sceneManagerAdapter.LoadSceneAsync(_scenes[scene], DispatchSceneLoadedEvent, mode);
        }
        
        public void LoadMazeScene(string sceneName, Action onComplete)
        {
            _sceneManagerAdapter.LoadSceneAsync(sceneName, onComplete, LoadSceneMode.Additive);
        }
        
        private void DispatchSceneLoadedEvent()
        {
            _eventDispatcher.Dispatch(new SceneLoadedSignal(_sceneLoaded));
        }
    }
}