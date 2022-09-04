using System;
using UnityEngine;

namespace InterfaceAdapters.Game
{
    public class GameCountdownTimer : IGameCountdownTimer
    {
        public event Action<float> OnCountdownUpdated;
        public event Action OnCountdownFinished;

        private float _initialCountdown;
        private float _currentCountdown;
        private float _startTime;
    
        public void SetInitialCountdown(float countdown)
        {
            _initialCountdown = countdown;
        }

        public void StartCountdown()
        {
            _startTime = Time.time;
            _currentCountdown = _initialCountdown;
            LaunchCountdownUpdated();
        }

        public void Reset()
        {
            _currentCountdown = _initialCountdown;
        }

        public void UpdateCountdown()
        {
            var elapsedTime = Time.time - _startTime;
            _currentCountdown = _initialCountdown - elapsedTime;
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
}