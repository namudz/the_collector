using System;
using UnityEngine.SceneManagement;

namespace InterfaceAdapters.Services.SceneLoader
{
    public interface ISceneManagerAdapter
    {
        void LoadSceneAsync(string sceneName, Action onSceneLoadedCallback, LoadSceneMode mode);
    }
}