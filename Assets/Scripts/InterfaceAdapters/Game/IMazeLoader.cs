using System;

namespace InterfaceAdapters.Game
{
    public interface IMazeLoader
    {
        void Load(string mazeSceneName, Action onComplete);
    }
}