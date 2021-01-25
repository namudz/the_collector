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

        private readonly IEventDispatcher _eventDispatcher;
        private SceneConstants.Scene _sceneLoaded;

        public SceneLoader(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public void LoadScene(SceneConstants.Scene scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            _sceneLoaded = scene;
            SceneManager.sceneLoaded += (arg0, sceneMode) =>
            {
                DispatchSceneLoadedEvent();
            };
            SceneManager.LoadSceneAsync(_scenes[scene], mode);
        }
        
        private void DispatchSceneLoadedEvent()
        {
            _eventDispatcher.Dispatch(new SceneLoadedSignal(_sceneLoaded));
        }
    }
}