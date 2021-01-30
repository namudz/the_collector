using System;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public interface ISceneLoader
    {
        void LoadScene(SceneConstants.Scene scene, LoadSceneMode mode = LoadSceneMode.Single);
        void LoadMazeScene(SceneConstants.Mazes mazeScene, Action onComplete);
    }
}