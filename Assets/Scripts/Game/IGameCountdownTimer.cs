using System;

public interface IGameCountdownTimer
{
    event Action<float> OnCountdownUpdated;
    void StartCountdown();
    void Reset();
}