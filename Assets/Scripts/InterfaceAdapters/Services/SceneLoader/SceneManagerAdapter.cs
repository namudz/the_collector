using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace InterfaceAdapters.Services.SceneLoader
{
    public class SceneManagerAdapter : ISceneManagerAdapter
    {
        private Action _sceneLoadedCallback;

        public SceneManagerAdapter()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }
        
        public void LoadSceneAsync(string sceneName, Action onSceneLoadedCallback, LoadSceneMode mode)
        {
            _sceneLoadedCallback = onSceneLoadedCallback;
            SceneManager.LoadSceneAsync(sceneName, mode);
        }

        private void HandleSceneLoaded(Scene _, LoadSceneMode __)
        {
            _sceneLoadedCallback?.Invoke();
        }
    }
}