using System;
using System.Collections;
using UnityEngine;

public class GameCountdownTimer : MonoBehaviour, IGameCountdownTimer
{
    public event Action<float> OnCountdownUpdated;

    private const float InitialCountdown = 15f;
    private float _currentCountdown;
    private float _startTime;

    private void Awake()
    {
        LaunchEvent();
    }

    public void Reset()
    {
        _currentCountdown = InitialCountdown;
    }
    
    public void StartCountdown()
    {
        _startTime = Time.time;
        _currentCountdown = InitialCountdown;
        LaunchEvent();
        StartCoroutine(UpdateCountdown());
    }

    private IEnumerator UpdateCountdown()
    {
        while (_currentCountdown >= 0f)
        {
            var elapsedTime = Time.time - _startTime;
            _currentCountdown = InitialCountdown - elapsedTime;
            LaunchEvent();
            yield return null;
        }
    }

    private void LaunchEvent()
    {
        OnCountdownUpdated?.Invoke(_currentCountdown);
    }

    
}