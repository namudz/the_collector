using System;
using UnityEngine.SceneManagement;

namespace InterfaceAdapters.Services.SceneLoader
{
    public class SceneManagerAdapter : ISceneManagerAdapter
    {
        public void LoadSceneAsync(string sceneName, Action onSceneLoadedCallback, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded += (arg0, sceneMode) =>
            {
                onSceneLoadedCallback?.Invoke();
            };
            SceneManager.LoadSceneAsync(sceneName, mode);
        }
    }
}