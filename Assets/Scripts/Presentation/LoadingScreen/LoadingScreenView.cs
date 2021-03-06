﻿using InterfaceAdapters.Signals;
using Services;
using Services.EventDispatcher;
using UnityEngine;

namespace Presentation.LoadingScreen
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Animator _spinnerAnimator;
        [SerializeField] private float _minVisibleTime = 1.5f;
        
        private IEventDispatcher _eventDispatcher;
        private float _showTime;

        private void Start()
        {
            DontDestroyOnLoad(this);
            _eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            _eventDispatcher.Subscribe<ShowLoadingScreenSignal>(HandleSignal);
            EnableCanvas(false);
        }

        private void OnDestroy()
        {
            _eventDispatcher.Unsubscribe<ShowLoadingScreenSignal>(HandleSignal);
        }

        private void HandleSignal(ISignal iSignal)
        {
            var signal = iSignal as ShowLoadingScreenSignal;
            
            if (signal.IsVisible)
            {
                _showTime = Time.time;
            }
            else
            {
                var diffTime = Time.time - _showTime;
                if (diffTime < _minVisibleTime)
                {
                    Invoke(nameof(DelayedHide), _minVisibleTime - diffTime);
                    return;   
                }
            }
            
            EnableCanvas(signal.IsVisible);
        }

        private void EnableCanvas(bool isVisible)
        {
            _canvas.enabled = isVisible;
            _spinnerAnimator.enabled = isVisible;
        }

        private void DelayedHide()
        {
            EnableCanvas(false);
            _eventDispatcher.Dispatch(new LoadingHideDelayedFinished());
        }
    }
}