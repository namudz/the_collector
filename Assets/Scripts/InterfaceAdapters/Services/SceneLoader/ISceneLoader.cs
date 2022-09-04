using System;
using UnityEngine.SceneManagement;

namespace InterfaceAdapters.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void LoadScene(SceneConstants.Scene scene, LoadSceneMode mode);
        void LoadMazeScene(string sceneName, Action onComplete);
    }
}