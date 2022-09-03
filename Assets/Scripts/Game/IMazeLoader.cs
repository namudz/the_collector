using System;

namespace Game
{
    public interface IMazeLoader
    {
        void Load(string mazeSceneName, Action onComplete);
    }
}