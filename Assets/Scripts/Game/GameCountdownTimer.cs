using System;
using UnityEngine;

public class GameCountdownTimer : IGameCountdownTimer
{
    public event Action<float> OnCountdownUpdated;
    public event Action OnCountdownFinished;

    private const float InitialCountdown = 10f;
    private float _currentCountdown;
    private float _startTime;

    public void StartCountdown()
    {
        _startTime = Time.time;
        _currentCountdown = InitialCountdown;
        LaunchCountdownUpdated();
    }
    
    public void Reset()
    {
        _currentCountdown = InitialCountdown;
    }

    public void UpdateCountdown()
    {
        var elapsedTime = Time.time - _startTime;
        _currentCountdown = InitialCountdown - elapsedTime;
        if (_currentCountdown <= 0f)
        {
            _currentCountdown = 0f;
            OnCountdownFinished?.Invoke();
        }

        LaunchCountdownUpdated();
    }

    private void LaunchCountdownUpdated()
    {
        OnCountdownUpdated?.Invoke(_currentCountdown);
    }
}