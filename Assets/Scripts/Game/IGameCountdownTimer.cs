using System;

public interface IGameCountdownTimer
{
    event Action<float> OnCountdownUpdated;
    event Action OnCountdownFinished;
    
    void SetInitialCountdown(float currentLevelCountdown);
    void StartCountdown();
    void UpdateCountdown();
    void Reset();
}