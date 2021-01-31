using System;

public interface IMazeLoader
{
    void Load(string mazeSceneName, Action onComplete);
    void SpawnElements();
    void Reset();
}