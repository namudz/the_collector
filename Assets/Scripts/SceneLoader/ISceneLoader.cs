﻿using System;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public interface ISceneLoader
    {
        void LoadScene(SceneConstants.Scene scene, LoadSceneMode mode = LoadSceneMode.Single);
        void LoadMazeScene(string sceneName, Action onComplete);
    }
}