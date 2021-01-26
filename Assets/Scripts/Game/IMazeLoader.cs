using System;

public interface IMazeLoader
{
    void Load(Action onComplete);
}